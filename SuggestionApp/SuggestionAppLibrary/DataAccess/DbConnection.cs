﻿using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace SuggestionAppLibrary.DataAccess;

public  class DbConnection
{
    private readonly IConfiguration _configuration;
    private readonly IMongoDatabase _mongoDatabase;
    private string _connectionId = "MongoDB";
    
    public string DbName { get; private set; } 
    public string CategoryCollectionName { get; private set; } = "categories";
    public string StatusCollectionName { get; private set; } = "statuses";
    public string UserCollectionName { get; private set; } = "users";
    public string SuggestionCollectionName { get; private set; } = "suggestions";
    public MongoClient Client { get; private set; }
    public IMongoCollection<CategoryModel> CategoryCollection { get; private set; }
    public IMongoCollection<StatusModel> StatusCollection { get; private set; }
    public IMongoCollection<UserModel> UserCollection { get; private set; }
    public IMongoCollection<SuggestionModel> SuggestionCollection { get; private set; }

    public DbConnection(IConfiguration configuration)
    {
        _configuration = configuration;
        Client = new MongoClient(_configuration.GetConnectionString(_connectionId));
        DbName = _configuration["DatabaseName"];
        _mongoDatabase = Client.GetDatabase(DbName);

        CategoryCollection = _mongoDatabase.GetCollection<CategoryModel>(CategoryCollectionName);
        StatusCollection = _mongoDatabase.GetCollection<StatusModel>(StatusCollectionName);
        UserCollection = _mongoDatabase.GetCollection<UserModel>(UserCollectionName);
        SuggestionCollection = _mongoDatabase.GetCollection<SuggestionModel>(SuggestionCollectionName);
    }
}