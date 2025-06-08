
# Problematica 

Durante desastres naturais, como enchentes ou frio extremo, abrigos enfrentam sérias dificuldades na organização e envio de alimentos. A falta de informações em tempo real sobre o estoque, condições de armazenamento e transporte dificulta a tomada de decisão, podendo levar à falta de alimentos ou desperdício devido à deterioração.

# Solução

### ZELUS: Sistema Inteligente para Monitoramento e Transporte de Alimentos em Abrigos Emergenciais

Desenvolvemos um sistema inteligente baseado em IoT que monitora continuamente peso, temperatura e status do transporte dos lotes de alimentos armazenados nos abrigos emergenciais.
Para apoiar essa solução, criamos uma API RESTful em ASP.NET Core que permite o cadastro, atualização e consulta dos abrigos com relacionamento de lotes de alimentos, facilitando a integração dos sensores IOT e sistemas de gestão.

---

## Funcionalidades da API

- Gerenciamento de Abrigos: cadastro, consulta, edição e exclusão.
- Controle de Lotes de Alimentos: registro de lotes com informações sobre nome, peso e vínculo com o abrigo.
- Documentação via Swagger: fácil teste e visualização dos endpoints.
- Persistência em banco de dados relacional com uso de migrations para versionamento da estrutura.
- Relacionamentos 1:N implementados entre Abrigos e seus Lotes.

### Tecnologias utilizadas

- ASP.NET Core Web API (.NET 9)
- Entity Framework Core com Oracle Database
- Swagger para documentação e testes
- Razor Pages com TagHelpers para interface web básica 

---

## Como rodar o projeto

1. Clone o repositório:
   ```bash
   git clone https://github.com/seuusuario/GlobalSolution.Net.git
   cd AbrigoGerenciamento
   ```

2. Configure a connection string do Oracle no arquivo `appsettings.json`:
   ```json
   "ConnectionStrings": {
     "OracleConnection": "User Id=seu_usuario;Password=sua_senha;Data Source=seu_host:porta/seu_servico"
   }
   ```

3. Crie o banco de dados e aplique as migrations:
   ```bash
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```

4. Execute a aplicação:
   ```bash
   dotnet run
   ```

5. Acesse a documentação Swagger (ou aplicação razor) para explorar e testar as rotas:
   ```
   http://localhost:5000/swagger/index.html
   http://localhost:5000/Abrigo
   ```
---
## Testes

Os testes foram realizados via Swagger e Aplicação Razor e incluem:

- Cadastro, edição, listagem e remoção de Abrigos
- Cadastro, edição, listagem e remoção de Lotes
- Registro de Lotes vinculados a um Abrigo
- Login com retorno de sucesso
  
![image](https://github.com/user-attachments/assets/b03db1e3-8874-4f41-9c69-baeba5079ada)
![image](https://github.com/user-attachments/assets/fec17539-c640-45db-97c8-0c8a0ce0e2de)

### Exemplo JSON para POST e PUT (Para abrigo e lote) 

```json
{
  "nome": "OngSos",
  "cep": "09756123",
  "email": "ongsos@gmail.com",
  "senha": "ongsos123"
}
{
  "nome": "Lote 1",
  "peso": 30
}
```

# Integrantes 
- Hellen Marinho Cordeiro, RM 558841
- Heloisa Alves de Mesquita, RM 559145
- Júlia Soares Farias dos Santos RM 554609


