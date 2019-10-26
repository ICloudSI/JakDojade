# JakDojade Web Api



Web Api for the JakDojade application. Provides information about stops, calculates the shortest route between two. User support has also been implemented - login and registration.

The Dijkstra Algorithm is used to calculate the route.

Logging in with JWT Token.


## Run
To run this project

```
$ cd ../JakDojade.Api
$ dotnet run
```

## Using

You need to load the data from the solvroCity.json file located in the JakDojade.Api folder. 
This requires logging in as an administrator. 
Sample admin account: 
```
email: admin@test.com
password: secret123
```
already existing. 

Adding from any file is planned.

Login generates JWT Token.

## Swagger

``` https://localhost:5001/swagger ```


