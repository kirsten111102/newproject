using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.UseDefaultFiles();
app.UseStaticFiles();

// 1. Database setup
string connectionString = "mongodb://localhost:27017"
    ?? throw new InvalidOperationException("Missing MongoDB connection string.");

var mongoClient = new MongoClient(connectionString);
var mongoDatabase = mongoClient.GetDatabase("unity_game");
var leaderboardCollection = mongoDatabase.GetCollection<ScoreEntry>("leaderboard");

// 2. Default initial data
var defaultLeaderboard = new List<ScoreEntry>
{
    new("Knight", 2500),
    new("Mage", 1800),
    new("Archer", 1500)
};

// Seed the collection if it's currently empty
if (!leaderboardCollection.Find(_ => true).Any())
{
    leaderboardCollection.InsertMany(defaultLeaderboard);
}

// 3. GET Endpoint: Retrieve top 5 scores
app.MapGet("/api/leaderboard", async () =>
{
    var sort = Builders<ScoreEntry>.Sort.Descending(s => s.Score);

    var players = await leaderboardCollection
        .Find(_ => true)
        .Sort(sort)
        .Limit(5)
        .ToListAsync();

    return Results.Ok(new { players });
});

// 4. POST Endpoint: Insert a new score
app.MapPost("/api/scores", async (ScoreEntry score) =>
{
    if (string.IsNullOrWhiteSpace(score.PlayerName) || score.Score < 0)
    {
        return Results.BadRequest(new { error = "Invalid score data" });
    }

    await leaderboardCollection.InsertOneAsync(score);

    return Results.Json(new { message = "Score saved" }, statusCode: 201);
});

app.Run("http://localhost:9999");

// Clean record used everywhere
[BsonIgnoreExtraElements]
public record ScoreEntry(string PlayerName, int Score);