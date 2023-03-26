![json cs](https://user-images.githubusercontent.com/73474137/227767534-b56e792c-9728-4100-87af-bea1aaec3eb1.png)

## Examples

```cs
Console.WriteLine(JSON.Parser.parse("[{  \"id\": 1,  \"first_name\": \"Jeanette\",  \"last_name\": \"Penddreth\",  \"email\": \"jpenddreth0@census.gov\",  \"gender\": \"Female\",  \"ip_address\": \"26.58.193.2\"}, {  \"id\": 2,  \"first_name\": \"Giavani\",  \"last_name\": \"Frediani\",  \"email\": \"gfrediani1@senate.gov\",  \"gender\": \"Male\",  \"ip_address\": \"229.179.4.212\"}, {  \"id\": 3,  \"first_name\": \"Noell\",  \"last_name\": \"Bea\",  \"email\": \"nbea2@imageshack.us\",  \"gender\": \"Female\",  \"ip_address\": \"180.66.162.255\"}, {  \"id\": 4,  \"first_name\": \"Willard\",  \"last_name\": \"Valek\",  \"email\": \"wvalek3@vk.com\",  \"gender\": \"Male\",  \"ip_address\": \"67.76.188.26\"}]").get(1).get("ip_address").get());
```

```sh
$ - (False, String, 229.179.4.212, 0, 0, False)
```

```cs
JSON.JSON test = JSON.Parser.parse("  [  {   \t\"id\": 1,  \"first_name\": \"Jeanette\",   \"last_name\": \"Penddreth\",  \"email\": \"jpenddreth0@census.gov\",  \"gender\": \"Female\",  \"ip_address\": \"26.58.193.2\"}, {  \"id\": 2,  \"first_name\": \"Giavani\",  \"last_name\": \"Frediani\",  \"email\": \"gfrediani1@senate.gov\",  \"gender\": \"Male\",  \"ip_address\": \"229.179.4.212\"}, {  \"id\": 3,  \"first_name\": \"Noell\",  \"last_name\": \"Bea\",  \"email\": \"nbea2@imageshack.us\",  \"gender\": \"Female\",  \"ip_address\": \"180.66.162.255\"}, {  \"id\": 4,  \"first_name\": \"Willard\",  \"last_name\": \"Valek\",  \"email\": \"wvalek3@vk.com\",  \"gender\": \"Male\",  \"ip_address\": \"67.76.188.26\"}  ]  ");
Console.WriteLine(test.ToString());
```

```sh
$ - [{"id":1,"first_name":"Jeanette","last_name":"Penddreth","email":"jpenddreth0@census.gov","gender":"Female","ip_address":"26.58.193.2"},{"id":2,"first_name":"Giavani","last_name":"Frediani","email":"gfrediani1@senate.gov","gender":"Male","ip_address":"229.179.4.212"},{"id":3,"first_name":"Noell","last_name":"Bea","email":"nbea2@imageshack.us","gender":"Female","ip_address":"180.66.162.255"},{"id":4,"first_name":"Willard","last_name":"Valek","email":"wvalek3@vk.com","gender":"Male","ip_address":"67.76.188.26"}]
```

## To-do

- [x] Better string formatting with `\`
- [ ] JSON class from file
- [x] `,` token error in Objects and Arrays
- [x] JSON class to JSON string
