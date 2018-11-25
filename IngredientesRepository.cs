using System;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage.Queue;
using System.Collections.Generic;

namespace AutNutriYA{
    public class IngredientesRepository{
        public const string STORAGEACCOUNTNAME = "s101moyag8";
        public const string ACCOUNTKEY = "WPB64UdtcYgJZ+d+EQW8v+LPrj0YkakcAsQXtE6KvOhMaTxuIaP+EqD7tXHpG3hqoKMlAWFwdLR2e1vWU57i+g==";

        public List<Ingrediente> LeerIngrediente(){
            var Table = ReferenciaTabla("Ingredientes");
            List<Ingrediente> Ingredientes = new List<Ingrediente>();
            TableQuery<IngredienteEntity> query = new TableQuery<IngredienteEntity>().Where
                (
                TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.NotEqual," ")
                );            
                
                CacharIngredientes();

                async void CacharIngredientes(){
                var list = new List<IngredienteEntity>();
                var tk = new TableContinuationToken();
                foreach (IngredienteEntity entity in await Table.ExecuteQuerySegmentedAsync(query,tk)){
                    
                    Ingredientes.Add(new Ingrediente(
                        entity.PartitionKey, 
                        entity.RowKey
                    ));
                }
            }

            
            return Ingredientes;
        }

        public bool CrearIngrediente(Ingrediente model){
            var Table = ReferenciaTabla("Ingredientes");

            Table.ExecuteAsync(TableOperation.Insert(new IngredienteEntity(model.Tipo,model.Nombre)));

            return true;
        }

        public bool ActualizarIngrediente(Ingrediente Paci)
        {
            var Table = ReferenciaTabla("Ingredientes");

            TableOperation retrieveOperation = TableOperation.Retrieve<IngredienteEntity>(Paci.Tipo,Paci.Nombre);

            EditarNutriologo();
                async void EditarNutriologo(){
                    TableResult retrievedResult = await Table.ExecuteAsync(retrieveOperation);
                    IngredienteEntity editEntity = (IngredienteEntity)retrievedResult.Result;
                    if (editEntity != null)
                    {

                        TableOperation editOperation = TableOperation.Replace(editEntity);

                        await Table.ExecuteAsync(editOperation);
                    }

                }

            return true;
        }

        public bool BorrarIngrediente(Ingrediente Paci){
            var Table = ReferenciaTabla("Ingredientes");
            TableOperation retrieveOperation = TableOperation.Retrieve<IngredienteEntity>(Paci.Tipo, Paci.Nombre);
            EliminarIngrediente();
            async void EliminarIngrediente(){
                TableResult retrievedResult = await Table.ExecuteAsync(retrieveOperation);
                IngredienteEntity deleteEntity = (IngredienteEntity)retrievedResult.Result;
                if(deleteEntity != null){
                    TableOperation deleteOperation = TableOperation.Delete(deleteEntity);
                    await Table.ExecuteAsync(deleteOperation);
                }
            }
            return true;

        }

        public IngredienteEntity PacEntTemp = new IngredienteEntity();
        public Ingrediente LeerPorPKRK(string PK, string RK)
            {
                
                Ingrediente IngredienteFin = new Ingrediente();
                var Table = ReferenciaTabla("Ingredientes");

                TableOperation retrieveOperation = TableOperation.Retrieve<IngredienteEntity>(PK, RK);

                CacharIngrediente();
                
                async void CacharIngrediente(){
                    TableResult retrievedResult = await Table.ExecuteAsync(retrieveOperation);
                    var TempPac = (IngredienteEntity)retrievedResult.Result;
                    try{
                    var PaciFin = new Ingrediente(
                        TempPac.PartitionKey,
                        TempPac.RowKey);
                    IngredienteFin = PaciFin;
                    }
                    catch{
                        
                    }
                }
               System.Threading.Thread.Sleep(500);
               return IngredienteFin;   
            }

        
        public CloudTable ReferenciaTabla(string NombreTabla){
            
            var StorageAccount = new CloudStorageAccount(new StorageCredentials(STORAGEACCOUNTNAME,ACCOUNTKEY),false);
            var tableClient = StorageAccount.CreateCloudTableClient();
            var Table = tableClient.GetTableReference(NombreTabla);
            Table.CreateIfNotExistsAsync();

            return Table;
        }
    }

    public class IngredienteEntity : TableEntity
    {
        public IngredienteEntity() { }
        public string Tipo => PartitionKey;
        public string Nombre => RowKey;

        public override string ToString() => $"{Tipo} {Nombre} ";
        public IngredienteEntity(string Tipo,string Nombre)
        {

            PartitionKey = Tipo;
            RowKey = Nombre;

        }
        
        
       
    }


}