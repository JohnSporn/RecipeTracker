﻿using Dapper;
using Microsoft.Data.Sqlite;
using System.Data;

namespace RecipeTracker.Data.Services
{
    public class DatabaseService
    {
        private readonly string _dbPath;

        public DatabaseService(string dbPath)
        {
            _dbPath = dbPath;
            DatabaseCreated();
        }

        private async void DatabaseCreated()
        {
            using var connection = new SqliteConnection($"Data Source={_dbPath}");

            string createTableRecipes = @"
                CREATE TABLE IF NOT EXISTS Recipe (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Title TEXT NOT NULL,
                    ServingSize TEXT NOT NULL,  
                    PrepTime TEXT NOT NULL,
                    CookTime TEXT NOT NULL,
                    Description TEXT NOT NULL,
                    Ingredients TEXT NOT NULL,
                    Instructions TEXT NOT NULL,
                    CategoryId INTEGER NOT NULL
                );   
            ";

            string createTableCategories = @"
                CREATE TABLE IF NOT EXISTS Category (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL
                );
            ";

            string createTableRecipeImage = @"
                CREATE TABLE IF NOT EXISTS RecipeImage (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Path TEXT NOT NULL,
                    Name TEXT NOT NULL, 
                    ContentType TEXT NOT NULL,  
                    RecipeId INTEGER NOT NULL
                );
            ";

            await connection.ExecuteAsync(createTableRecipes);
            await connection.ExecuteAsync(createTableCategories);
            await connection.ExecuteAsync(createTableRecipeImage);
        }

        public IDbConnection CreateConnection() => new SqliteConnection($"Data Source={_dbPath}");
    }
}
