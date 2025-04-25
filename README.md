# Holiday Source

A back-end holiday server.

## Set Up

Build:

```
docker build -t holiday-be .
```

Run:

```
docker run --rm -p 5254:5254 holiday-be
```

Holiday information in JSON format can be retrieved via GET request to [localhost:5254](http://localhost:5254/).

## To Do

- Error handling
- Favorite country management
- Searching/filtering