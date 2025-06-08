
# Solução ZELUS: Sistema Inteligente para Monitoramento e Transporte de Alimentos em Abrigos Emergenciais

Este projeto oferece um ambiente com dois containers integrados, sendo: 
- Rodar uma aplicação .NET
- Persistir os dados em um Banco de Dados Oracle

Desenvolvemos um sistema inteligente baseado em IoT que monitora continuamente peso, temperatura e status do transporte dos lotes de alimentos armazenados nos abrigos emergenciais.
Para apoiar essa solução, criamos uma API RESTful em ASP.NET Core, permitindo o cadastro, atualização e consulta de abrigos com seus respectivos lotes de alimentos, facilitando a integração com sensores IoT e sistemas de gestão emergencial.
A aplicação foi conteinerizada em uma máquina virtual com Docker, atendendo aos requisitos da disciplina de DevOps, com:
- Um container para a aplicação .NET;
- Um container para o Banco de Dados Oracle (para persistência dos dados);

---

# Aplicação da solução em containers 

### Link do Video exemplificando
https://www.youtube.com/watch?v=s_gESK5a7Xk

### Abrindo o projeto 
```bash
# Clone o projeto
git clone https://github.com/hmarinhoo/ZELUS.GS.Cloud
cd AbrigoGerenciamento
```

### Build e push da imagem para Docker Hub
```bash
docker build -t hellomesq/abrigogerenciamento:latest .
docker login
docker push hellomesq/abrigogerenciamento:latest
```

### Criação da VM e Deploy na Azure CLI
```bash
chmod +x deploy.sh
./deploy.sh
```
```bash
ssh azureuser@IPDAVM
Senha: ZelusGs2025!
```

### Instalar Docker na VM
```bash
sudo apt update
sudo apt install -y docker.io
sudo systemctl start docker
sudo systemctl enable docker
sudo usermod -aG docker $USER
```

### Saia e entre novamente da VM
```bash
exit
ssh azureuser@IPDAVM
Senha: ZelusGs2025!
```

### Criando o container da Oracle

```bash
docker login container-registry.oracle.com
```
```bash
docker network create oracle-network
```
```bash
docker run -d \
  --name oracle-db \
  --network oracle-network \
  -p 1521:1521 \
  -e ORACLE_PWD=Oracle123 \
  -v oracle_data:/opt/oracle/oradata \
  container-registry.oracle.com/database/express:21.3.0-xe
```

### Alterando usuário caso esteja na padrão
```bash
docker exec -it oracle-db bash
sqlplus system/Oracle123@//localhost:1521/XEPDB1
CREATE USER appuser IDENTIFIED BY Oracle123;
GRANT CONNECT, RESOURCE TO appuser;
ALTER USER appuser DEFAULT TABLESPACE USERS;
ALTER USER appuser QUOTA UNLIMITED ON USERS;
```

### Rodando a imagem do DockerHub
```bash
docker run -d \
  --name gsabrigogerenciamento \
  --network oracle-network \
  -e "ConnectionStrings__DefaultConnection=User Id=appuser;Password=Oracle123;Data Source=oracle-db:1521/XEPDB1" \
  -p 5000:5000 \
  hellomesq/abrigogerenciamento:latest
```

### Verificação dos containers ativos
```bash
docker ps
```

### Abrindo no navegador 
```bash
http://<IP_PÚBLICO_DA_VM>:5000/swagger/index.html
http://<IP_PÚBLICO_DA_VM>:5000/Abrigo
```

### Acessando o container do Banco de Dados
```bash
docker exec -it oracle-db bash
sqlplus appuser/Oracle123@//localhost:1521/XEPDB1
```

### Caso dê erro nas tabelas, apague e crie novamente
- APAGAR TABELA:
- ```bash
  SELECT table_name FROM user_tables;
  DROP TABLE Abrigos CASCADE CONSTRAINTS;
  DROP TABLE LotesAlimentos CASCADE CONSTRAINTS;
  DROP TABLE "__EFMigrationsHistory" CASCADE CONSTRAINTS;
  ```

- RODAR LOCALMENTE DE NOVO:
- ```bash
  dotnet ef database update
  ```
  
# Integrantes 
- Hellen Marinho Cordeiro, RM 558841
- Heloisa Alves de Mesquita, RM 559145
- Júlia Soares Farias dos Santos RM 554609

