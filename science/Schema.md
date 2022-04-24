Database schema:
```js
Database = {
    "Location": {
        "key": "Int32",
        "Region": "String",
        "Town": "String",
        "District": "String"
    },
    "Credential": {
        "key": "String",
        "Password": "String",
        "Person": "Int32"
    },
    "Person": {
        "key": "Int32",
        "Surname": "String",
        "Name": "String",
        "Location": "Int32",
        "RegDate": "DateTime"
    },
    "EstateObject": {
        "key": "Int32",
    },
    "ClientWish": {
        "key": "Int32"
    },
    "Bookmark": {
        "key": "Int32"
    },
    "Match": {
        "key": "Int32"
    },
    "Order": {
        "key": "Int32"
    },
    "Deal": {
        "key": "Int32"
    }
}

BackingTypes = {
    "DateTime": ["String"],
    "Int32": ["Number", "Integer", "Int"]
}
```
