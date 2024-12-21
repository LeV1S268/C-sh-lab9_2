using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Security.Policy;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json.Nodes;

namespace WeatherApp
{
    public partial class Form1 : Form
    {
        // API ключ для доступа к OpenWeatherMap API
        private readonly string apiKey = "11fa82747ccc6c06003115d1dc541888";

        // URL для API
        private readonly string apiUrl = "https://api.openweathermap.org/data/2.5/weather";

        // Путь к файлу с городами
        private readonly string cityPath = "C:\\Users\\aleks\\OneDrive\\Рабочий стол\\Прога\\СЕМ 3\\lab9_2\\city.txt";

        // Список городов
        private readonly List<City> _cities = new List<City>();

        public Form1()
        {
            InitializeComponent(); // Инициализация формы
            LoadCitiesAsync(cityPath); // Загрузка городов из файла
        }

        // Асинхронная загрузка списка городов из файла
        private async Task LoadCitiesAsync(string filePath)
        {
            try
            {
                // Чтение всех строк из файла
                var cities = File.ReadAllLines(filePath);

                foreach (var city in cities)
                {
                    // Разделение строки на имя города и координаты
                    var parts = city.Split('\t');
                    if (parts.Length == 2)
                    {
                        var name = parts[0];
                        var coord = parts[1].Replace(" ", "").Split(',');

                        // Преобразование координат в числовой формат
                        var latitude = Convert.ToDouble(coord[0].Replace(".", ","));
                        var longitude = Convert.ToDouble(coord[1].Replace(".", ","));

                        // Создание объекта City
                        var info = new City(name, latitude, longitude);
                        _cities.Add(info);
                    }
                }

                // Привязка списка городов к ComboBox
                CityComboBox.DataSource = _cities;
            }
            catch (Exception ex)
            {
                // Обработка ошибок при чтении файла
                MessageBox.Show($"Error loading cities: {ex.Message}");
            }
        }

        // Обработчик события нажатия на кнопку Get Weather
        private async void GetWeatherButton_Click(object sender, EventArgs e)
        {
            // Проверка, выбран ли город в ComboBox
            if (CityComboBox.SelectedItem is City selectedCity)
            {
                try
                {
                    // Получение данных о погоде
                    var weather = await FetchWeatherAsync(apiUrl, selectedCity);

                    if (weather != null)
                    {
                        // Вывод данных о погоде в текстовое поле
                        ResulttextBox.Text = weather.ToString();
                    }
                    else
                    {
                        MessageBox.Show("Failed fetching weather data. Try again later.");
                    }
                }
                catch (Exception ex)
                {
                    // Обработка ошибок при запросе данных
                    MessageBox.Show($"Error occured: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Please choose a city");
            }
        }

        // Асинхронный метод для получения данных о погоде
        private async Task<Weather> FetchWeatherAsync(string URL, City city)
        {
            try
            {
                // Настройка HTTP-клиента
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(URL);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Формирование параметров запроса
                var urlParameters = $"?lat={city.Latitude}&lon={city.Longitude}&appid={apiKey}&units=metric";
                var fullUrl = URL + urlParameters;

                // Отправка запроса
                var response = await client.GetAsync(fullUrl);

                if (response.IsSuccessStatusCode)
                {
                    // Чтение ответа и парсинг JSON
                    var responseString = await response.Content.ReadAsStringAsync();
                    var json = JsonObject.Parse(responseString);

                    // Создание объекта Weather из ответа
                    Weather res = new Weather();
                    res.Country = (string)json["sys"]["country"];
                    res.Name = (string)json["name"];
                    res.Temp = (double)json["main"]["temp"];
                    res.Description = (string)json["weather"][0]["main"];
                    return res;
                }
                else
                {
                    MessageBox.Show($"API error: {response.StatusCode}");
                }
                return null;
            }
            catch (Exception ex)
            {
                // Обработка ошибок при запросе
                MessageBox.Show($"Failed fetching weather data: {ex.Message}");
                return null;
            }
        }
    }

    // Класс для описания города
    public class City
    {
        public string Name { get; } // Название города
        public double Latitude { get; } // Широта
        public double Longitude { get; } // Долгота

        public City(string name, double latitude, double longitude)
        {
            Name = name;
            Latitude = latitude;
            Longitude = longitude;
        }

        public override string ToString()
        {
            return Name; // Возвращает название города для отображения в ComboBox
        }
    }

    // Класс для описания данных о погоде
    public class Weather
    {
        public string Country { get; set; } // Страна
        public string Name { get; set; } // Город
        public double Temp { get; set; } // Температура
        public string Description { get; set; } // Описание погоды

        public Weather(string country, string name, double temp, string description)
        {
            Country = country;
            Name = name;
            Temp = temp;
            Description = description;
        }

        public Weather()
        {
        }

        public override string ToString()
        {
            // Форматированный вывод данных о погоде
            return $"Country: {Country}, City: {Name},Temperature: {Temp} °C, Description: {Description}";
        }
    }
}
