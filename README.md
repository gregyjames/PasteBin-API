![alt tag](http://i.imgur.com/lDXZByY.png)

# PasteBin-API

A C# Wrapper for the pastebin api written to be fast and efficient.

# Sample Usage

### Initialize
```csharp
var core = new PasteBinCore("DEVAPIKEY");
var user = new PasteBinCore.User("USERNAME", "PASSWORD", core);
```
### Post Anon
```csharp
core.PostAnnon("PostName", "PostText");
```
### Post as user
```csharp
core.PostAsUser("PostName", "PostText", user);
```
### Get All posts from user
```csharp
//Gets the 10 most recent posts for user
var postes = user.getUserPosts(10);

//Displays title of first 2 posts
Console.WriteLine(postes.Paste[0].Paste_title);
Console.WriteLine(postes.Paste[1].Paste_title);
```
