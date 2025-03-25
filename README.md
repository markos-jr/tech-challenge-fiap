# **ContactManager**

O **ContactManager** é uma aplicação baseada em .NET que permite gerenciar contatos, oferecendo funcionalidades como cadastro, consulta, atualização e exclusão de contatos. O projeto foi desenvolvido seguindo os princípios de **Clean Architecture**, utilizando Entity Framework Core para persistência de dados e testes unitários para garantir a qualidade do código.

---

## **Tecnologias Utilizadas**

- **.NET 8.0**
- **Entity Framework Core**
- **SQL Server (Azure)**
- **xUnit** (Testes Unitários)
- **MoQ** (Mocking nos Testes)
- **Swagger** (Documentação de API)
- **MemoryCache** (Cache interno para otimização de consultas)

-----

## **Estrutura do Projeto**

O projeto segue a arquitetura em camadas com **Clean Architecture**, conforme descrito abaixo:



---

## **Endpoints da API**

### **Base URL:** 
`https://localhost:5001/api/contacts`

### **1. Consultar Contatos**
- **GET** `/`
  - Retorna todos os contatos.
  - Exemplo de resposta:
    ```json
    [
        {
            "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
            "name": "John Doe",
            "phone": "123456789",
            "email": "johndoe@example.com",
            "ddd": "21"
        }
    ]
    ```

- **GET** `/?ddd=21`
  - Filtra contatos pelo DDD.

---

### **2. Cadastrar Contato**
- **POST** `/`
  - Corpo da Requisição:
    ```json
    {
        "name": "John Doe",
        "phone": "123456789",
        "email": "johndoe@example.com",
        "ddd": "21"
    }
    ```
  - Resposta:
    ```json
    {
        "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "name": "John Doe",
        "phone": "123456789",
        "email": "johndoe@example.com",
        "ddd": "21"
    }
    ```

---

### **3. Atualizar Contato**
- **PUT** `/{id}`
  - Corpo da Requisição:
    ```json
    {
        "name": "Jane Doe",
        "phone": "987654321",
        "email": "janedoe@example.com",
        "ddd": "11"
    }
    ```

---

### **4. Excluir Contato**
- **DELETE** `/{id}`

---

## **Configuração do Ambiente**

### **1. Pré-requisitos**
- **SDK .NET 8.0**
- **SQL Server**
- **Visual Studio 2022**

### **2. Configuração do Banco de Dados**
- Configure a string de conexão no arquivo `appsettings.json`:
  ```json
  {
      "ConnectionStrings": {
          "DefaultConnection": "Server=tcp:<seu-servidor>.database.windows.net,1433;Database=ContactManagerDB;User ID=<usuario>;Password=<senha>;Encrypt=True;"
      }
  }




