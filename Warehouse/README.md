# About this project
## Introduction
This project is a basic REST API that could be used in a warehouses business. 
It was written in two different approaches: entitty framework and using sql connections
## Structure

### Controller

#### GET (api/owners/id)
This method allows user to get a client with given id along with all of the objects he owns from all warehouses.
It has been written using entitty framework.

#### GET (api/owners/id/SQL)
This method allows user to get a client with given id along with all of the objects he owns from all warehouses.
It has been written using sql connection.

#### POST (api/owners/ownersID/objects/objectsID)
This method allows user to assign objects ownership to a client.
It has been written using entitty framework.

#### POST (api/owners/ownersID/objects/objectsID/SQL)
This method allows user to assign objects ownership to a client.
It has been written using sql connection.

### Service
With a usage of dependency injection it serves the controller to handle all of the tasks

1. GetOwnersObjects(int id);
    - Returns owner with given id and all of his objects from every warehouse.
2. GetOwnersObjectsSQL(int id);
    - Returns owner with given id and all of his objects from every warehouse. SQL version.
3. DoesOwnerExist(int id);
    - Checks if owner with given id exists in database.
4. DoesOwnerExistSQL(int id);
    - Checks if owner with given id exists in database. SQL version.
5. DoesObjectExist(int id);
    - Checks if object with given id exists in database.
6. DoesObjectExistSQL(int id);
    - Checks if object with given id exists in database. SQL version.
7. AddObjectOwner(int IdOwner, int IdObject);
    - Assigns object with given id to a client with given id.
8. AddObjectOwnerSQL(int IdOwner, int IdObject);
    - Assigns object with given id to a client with given id. SQL version.
9. DoesPairExist(int IdOwner, int IdObject);
    - Checks if client already owns an object with given id.
10. DoesPairExistSQL(int IdOwner, int IdObject);
    - Checks if client already owns an object with given id. SQL version.

## What I learned
This project taught me both how to work with entitty framework and sql connetions. 
It also showed me how to work with and retrive data from multiple tables at once.
