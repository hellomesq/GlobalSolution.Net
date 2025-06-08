
# ZELUS - Sistema Inteligente para Monitoramento e Transporte de Alimentos em Abrigos Emergenciais

Para apoiar essa solução, criamos uma API RESTful em ASP.NET Core que permite o cadastro, atualização e consulta dos abrigos com relacionamento de lotes de alimentos, facilitando a integração dos sensores IoT e sistemas de gestão.
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
## Diagrama

---
## Testes

Os testes foram realizados via Swagger e Aplicação Razor e incluem:

- Cadastro, edição, listagem e remoção de Abrigos
- Registro de Lotes vinculados a um Abrigo
  
![image](https://github.com/user-attachments/assets/b03db1e3-8874-4f41-9c69-baeba5079ada)
![image](https://github.com/user-attachments/assets/ce0a484c-cbcb-4c6c-a869-4b9884809874)

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



