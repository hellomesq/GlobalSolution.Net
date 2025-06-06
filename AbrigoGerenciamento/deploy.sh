#!/bin/bash

RESOURCE_GROUP="gszelus"
VM_NAME="vmgszelus"
LOCATION="eastus"
ADMIN_USERNAME="azureuser"
ADMIN_PASSWORD="ZelusGs2025!"  
APP_PORT=5000
DB_PORT=1521

echo "Criando Resource Group..."
az group create --name $RESOURCE_GROUP --location $LOCATION

echo "Criando VM com senha..."
az vm create \
  --resource-group $RESOURCE_GROUP \
  --name $VM_NAME \
  --image Ubuntu2204 \
  --admin-username $ADMIN_USERNAME \
  --authentication-type password \
  --admin-password $ADMIN_PASSWORD \
  --output json

echo "Abrindo portas $APP_PORT e $DB_PORT..."
az vm open-port --resource-group $RESOURCE_GROUP --name $VM_NAME --port $APP_PORT --priority 1001
az vm open-port --resource-group $RESOURCE_GROUP --name $VM_NAME --port $DB_PORT --priority 1002


IP=$(az vm show -d -g $RESOURCE_GROUP -n $VM_NAME --query publicIps -o tsv)
echo "IP público da VM: $IP"
echo "Conecte-se via SSH:"
echo "ssh $ADMIN_USERNAME@$IP"
echo "Senha: $ADMIN_PASSWORD"
