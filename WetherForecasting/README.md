Weather Forecasting Web Application

Overview
This project is a weather forecasting web application built using C#.Net and ASP.Net MVC . It allows users to upload a CSV file containing location data, processes the data, fetches weather forecast information from Open-Meteo API, and displays the forecasted weather for the uploaded locations in a user-friendly UI.

Features

CSV File Upload: Users can upload a CSV file containing location data including Latitude, Longitude, and Location Name.
Data Processing: The application processes the uploaded CSV file to extract location information.
Weather Forecast Retrieval: It makes use of the Open-Meteo API to fetch weather forecast data for the uploaded locations.
UI Display: Users are presented with a list of uploaded locations and the forecasted weather for the next few days in an intuitive UI.
Exception Handling: The application includes error handling and validation, such as checking for null values in the uploaded CSV file to avoid exceptions.
Getting Started
To run the application locally, follow these steps:

Clone the repository to your local machine.

Open the solution in Visual Studio.
Configure the API key for Open-Meteo or any other weather service provider in the appropriate location (replace "your_api_key_here").
Build and run the application.
Project Structure
Models: Contains the LocationModel class for storing location data.
Controllers: Includes the HomeController with actions for file upload and weather forecast retrieval.
Views: Contains the Index.cshtml view for displaying the UI.
Tests: Includes unit tests for file upload and exception handling.
Testing
The application includes unit tests to ensure functionality:

File Upload Test: Checks if the CSV file upload functionality is working correctly.
Exception Handling Test: Verifies that the application handles null values in the uploaded CSV file without throwing exceptions.

Dependencies
ASP.Net MVC
Newtonsoft.Json for JSON parsing
HttpClient for API calls