
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

