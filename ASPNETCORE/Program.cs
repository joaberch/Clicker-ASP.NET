using MongoDB.Bson;
using MongoDB.Driver;
using System.Reflection.Metadata;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Récupérer votre chaîne de connexion MongoDB depuis les paramètres de configuration
string mongoConnectionString = builder.Configuration.GetConnectionString("MongoDBConnection");
string connectionString = "mongodb://127.0.0.1:32778";

// Configurez votre client MongoDB
builder.Services.AddSingleton<IMongoClient>(new MongoClient(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

//MongoDB connection
// Récupérez votre client MongoDB
var serviceProvider = app.Services;
var client = new MongoClient(connectionString);
//var client = serviceProvider.GetRequiredService<IMongoClient>();

// Accédez à une base de données spécifique
var database = client.GetDatabase("test");

// Accédez à une collection spécifique dans cette base de données
var collection = database.GetCollection<BsonDocument>("test");

// Insérez des documents dans la collection
var document = new BsonDocument
{
	{ "compteur", 0 }
};

var cptr = new Cptr { counter = 0 };
collection.InsertOneAsync(document);

app.Run();

public class Cptr
{
	public ObjectId _id { get; set; }
	public int counter { get; set; }
}