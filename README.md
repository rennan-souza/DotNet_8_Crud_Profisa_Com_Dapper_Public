# CRUD Profisa usando Dapper e PostgreSQL

### Descrição
* Projeto simples para estudar boas práticas com SQL, Exceções personalizadas e Validações

### Criar tabela no banco de dados
* Para criar a tabela de `produtos`, execute o seguinte código SQL:

```
CREATE TABLE produtos (
    id SERIAL PRIMARY KEY,
    sku VARCHAR(50) NOT NULL UNIQUE,
    nome VARCHAR(100) NOT NULL,
    preco DECIMAL(10, 2),
    qtd_estoque INT,
    data_fabricacao TIMESTAMP
);
```

* Primeiros inserts na tabela de `produtos` para teste

```
insert into produtos (sku, nome, preco, qtd_estoque, data_fabricacao)
values ('PROD-1', 'Produto Um', 2.99, 88, '2024-04-01');

insert into produtos (sku, nome, preco, qtd_estoque, data_fabricacao)
values ('PROD-2', 'Produto Dois', 49.90, 10, '2024-10-12');

insert into produtos (sku, nome, preco, qtd_estoque, data_fabricacao)
values ('PROD-3', 'Produto Três', 99.00, 11, '2024-08-10');
```

### Configurações
* Em `appsettings.json` você deve colocar as informações de conexão com o seu banco PostgreSQL local de teste, lembre-se de não expor
dados sensíveis, as configurações do seu banco de produção devem ficar nas variaveis de ambiente do seu servidor seguindo as nomeclatura 
corretas que você também pode ver na classe `PostgreSqlConnection.cs` dentro do diretório `Connections`

### Exemplo de requisição para cadastrar um produto

* Exemplo 1 informando todos os campos

```
{
  "sku": "Tese 01",
  "nome": "Teste 01",
  "preco": 9.99,
  "qtdEstoque": 10,
  "dataFabricacao": "2024-10-12"
}
```

* Exemplo 2 informando apenas os campos obrigatórios

```
{
  "sku": "1234",
  "nome": "Teste com campos opcionais",
  "preco": null,
  "qtdEstoque": null,
  "dataFabricacao": null
}
```