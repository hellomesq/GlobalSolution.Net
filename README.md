
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


