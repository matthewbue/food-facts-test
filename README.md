
# FitnessFoods API

## Descrição do Projeto

Este projeto é uma API desenvolvida para importar, processar e gerenciar dados de produtos alimentícios. Utilizando dados de um desafio fornecido pela **Coodesh**, a API oferece funcionalidades como importação de produtos em lote, listagem de produtos e manipulação de dados no banco de dados MongoDB.

## Tecnologias Utilizadas

- **Linguagem:** C# (.NET 9.0)
- **Framework:** ASP.NET Core 9.0
- **Banco de Dados:** MongoDB
- **Testes Unitários:** xUnit, Moq
- **Infraestrutura:** Docker, Docker Compose
- **API:** RESTful

## Como Instalar e Usar o Projeto

### 1. **Clonando o Repositório**

Clone o repositório para a sua máquina local:

```bash
git clone https://github.com/usuario/FitnessFoodsApi.git
cd FitnessFoodsApi
```

### 2. **Instalando Dependências (Modo Manual)**

Restabeleça as dependências do projeto:

```bash
dotnet restore
```

### 3. **Compilando e Executando o Projeto (Modo Manual)**

Para compilar e rodar a aplicação manualmente no seu ambiente local:

1. Abra o terminal ou prompt de comando e execute o comando abaixo para rodar a aplicação:

   ```bash
   dotnet run
   ```

2. O projeto será iniciado no `http://localhost:5000`.

   **Nota:** Caso você queira rodar a aplicação em outra porta, altere as configurações no arquivo `appsettings.json` ou passe um argumento de porta ao executar a aplicação:

   ```bash
   dotnet run --urls "http://localhost:8080"
   ```

### 4. **Rodando o Projeto com Docker**

#### Pré-requisitos:

Certifique-se de ter o Docker instalado na sua máquina. Se não tiver, [instale o Docker](https://docs.docker.com/get-docker/) primeiro.

#### Passos:

1. **Criando a Imagem Docker:**

   No terminal, navegue até a raiz do projeto e execute o comando abaixo para criar a imagem Docker:

   ```bash
   docker build -t fitnessfoodsapi .
   ```

2. **Executando o Projeto com Docker:**

   Agora, execute o Docker Compose para subir a aplicação e o MongoDB em containers separados:

   ```bash
   docker-compose up -d
   ```

3. **Verificando os Containers:**

   Após rodar o comando acima, o Docker irá iniciar os containers. Para verificar se tudo está rodando corretamente, use:

   ```bash
   docker ps
   ```

4. **Acessando a Aplicação:**

   A API estará disponível no endereço `http://localhost:5000`. Para garantir que tudo foi configurado corretamente, abra o navegador e acesse a URL.

#### Docker Compose

Aqui está o arquivo `docker-compose.yml` que será utilizado para rodar o MongoDB e a aplicação:

```yaml
version: '3.4'

services:
  fitnessfoodsapi:
    image: fitnessfoodsapi
    build:
      context: .
    ports:
      - "5000:5000"
    environment:
      - ASPNETCORE_ENVIRONMENT=Development
    depends_on:
      - mongo
    networks:
      - fitnessfoods_network

  mongo:
    image: mongo:latest
    container_name: mongo_db
    ports:
      - "27017:27017"
    volumes:
      - mongo_data:/data/db
    networks:
      - fitnessfoods_network

volumes:
  mongo_data:

networks:
  fitnessfoods_network:
    driver: bridge
```

#### Parâmetros de Configuração

- **Portas:**
  - **API**: `5000`
  - **MongoDB**: `27017` (padrão do MongoDB)

#### Como Configurar Variáveis de Ambiente:

Se você precisa alterar configurações como a string de conexão do banco de dados, você pode adicionar um arquivo `.env` na raiz do projeto. O Docker irá utilizar as variáveis definidas nesse arquivo para configurar o ambiente da aplicação.

Exemplo de conteúdo do arquivo `.env`:

```env
MONGO_CONNECTION_STRING=mongodb://mongo:27017/FitnessFoodsDb
```

O arquivo `.env` será carregado automaticamente pelo Docker, configurando as variáveis para sua aplicação.

### 5. **Como Usar a API**

#### Importação de Produtos

Para importar produtos para o sistema, utilize o endpoint `POST /api/import` com uma URL válida que aponte para um arquivo JSON comprimido.

**Exemplo de requisição:**

```http
POST http://localhost:5000/api/import?url=https://link-to-product-file.com
```

#### Listar Produtos

Para listar produtos, use o endpoint `GET /api/products`.

**Exemplo de requisição:**

```http
GET http://localhost:5000/api/products?page=1&pageSize=10
```

#### Obter Produto por Código

Para obter detalhes de um produto pelo código, use o endpoint `GET /api/products/{code}`.

**Exemplo de requisição:**

```http
GET http://localhost:5000/api/products/0000000000017
```

#### Atualizar Produto

Para atualizar um produto existente, use o endpoint `PUT /api/products/{code}`. O corpo da requisição deve conter as informações do produto a ser atualizado.

**Exemplo de requisição:**

```http
PUT http://localhost:5000/api/products/0000000000017
Content-Type: application/json

{
  "product_name": "Updated Product Name",
  "quantity": "Updated Quantity"
}
```

#### Excluir Produto

Para excluir ou marcar um produto como "excluído", use o endpoint `DELETE /api/products/{code}`.

**Exemplo de requisição:**

```http
DELETE http://localhost:5000/api/products/0000000000017
```

## .gitignore

Este repositório contém um arquivo `.gitignore` que ignora arquivos temporários e específicos de IDE, como:

```gitignore
# Build Folders
bin/
obj/

# User-specific files
.vscode/
.idea/

# User-specific files generated by Visual Studio
.vs/
*.user

# IDE specific files
*.suo
*.userosscache
*.sln.docstates
```

## Licença

Este projeto está licenciado sob a [MIT License](LICENSE).

## Desafio by Coodesh

Este projeto faz parte de um desafio promovido pela **Coodesh** para avaliação de habilidades em desenvolvimento de software e implementação de APIs com .NET e MongoDB.
