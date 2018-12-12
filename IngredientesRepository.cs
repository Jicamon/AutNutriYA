using System;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage.Queue;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutNutriYA{
    public class IngredientesRepository{
        public const string STORAGEACCOUNTNAME = "s101moyag8";
        public const string ACCOUNTKEY = "WPB64UdtcYgJZ+d+EQW8v+LPrj0YkakcAsQXtE6KvOhMaTxuIaP+EqD7tXHpG3hqoKMlAWFwdLR2e1vWU57i+g==";

        public async Task<List<Ingrediente>> LeerIngrediente(){
            var Table = ReferenciaTabla("Ingredientes");
            List<Ingrediente> Ingredientes = new List<Ingrediente>();
            TableQuery<IngredienteEntity> query = new TableQuery<IngredienteEntity>().Where
                (
                TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.NotEqual," ")
                );            
                
                var Res = await CacharIngredientes();

                async Task<List<Ingrediente>> CacharIngredientes(){
                var list = new List<IngredienteEntity>();
                var tk = new TableContinuationToken();
                foreach (IngredienteEntity entity in await Table.ExecuteQuerySegmentedAsync(query,tk)){
                    
                    Ingredientes.Add(new Ingrediente(
                        entity.PartitionKey, 
                        entity.RowKey
                    ));
                }
                return Ingredientes;
            }

            
            return Res;
        }

        public async Task<Ingrediente> LeerIngrediente(string RowKey){
            Ingrediente ingrediente = new Ingrediente();
            IngredienteEntity ingrediente2 = new IngredienteEntity();
            var Table = ReferenciaTabla("Ingredientes");
            TableQuery<IngredienteEntity> query = new TableQuery<IngredienteEntity>().Where(
                TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, RowKey));

            var Res = await CacharIngrediente();

            async Task<Ingrediente> CacharIngrediente(){
                var list = new List<IngredienteEntity>();
                var tk = new TableContinuationToken();
                foreach (IngredienteEntity entity in await Table.ExecuteQuerySegmentedAsync(query, tk)){
                    
                    var newIngrediente2 = new Ingrediente(
                        entity.PartitionKey, 
                        entity.RowKey);

                    ingrediente = newIngrediente2;
                    

                }
                return ingrediente;
            }
            return Res;
            
            
        }

        public bool CrearIngrediente(Ingrediente model){
            var Table = ReferenciaTabla("Ingredientes");

            Table.ExecuteAsync(TableOperation.Insert(new IngredienteEntity(model.Tipo,model.Nombre)));

            return true;
        }

        public async Task<bool> ActualizarIngrediente(Ingrediente Paci)
        {
            var Table = ReferenciaTabla("Ingredientes");

            TableOperation retrieveOperation = TableOperation.Retrieve<IngredienteEntity>(Paci.Tipo,Paci.Nombre);

            var funciono = await EditarNutriologo();
                async Task<bool> EditarNutriologo(){
                    TableResult retrievedResult = await Table.ExecuteAsync(retrieveOperation);
                    IngredienteEntity editEntity = (IngredienteEntity)retrievedResult.Result;
                    if (editEntity != null)
                    {

                        TableOperation editOperation = TableOperation.Replace(editEntity);

                        await Table.ExecuteAsync(editOperation);
                        return true;
                    }else return false;

                }

            return funciono;
        }

        public async Task<bool> BorrarIngrediente(Ingrediente Paci){
            var Table = ReferenciaTabla("Ingredientes");
            TableOperation retrieveOperation = TableOperation.Retrieve<IngredienteEntity>(Paci.Tipo, Paci.Nombre);
            
            var fucniono = await EliminarIngrediente();
            async Task<bool> EliminarIngrediente(){
                TableResult retrievedResult = await Table.ExecuteAsync(retrieveOperation);
                IngredienteEntity deleteEntity = (IngredienteEntity)retrievedResult.Result;
                if(deleteEntity != null){
                    TableOperation deleteOperation = TableOperation.Delete(deleteEntity);
                    await Table.ExecuteAsync(deleteOperation);
                    return true;
                } else return false;
            }
            return fucniono;

        }

        public IngredienteEntity PacEntTemp = new IngredienteEntity();
        public async Task<Ingrediente> LeerPorPKRK(string PK, string RK)
            {
                
                Ingrediente IngredienteFin = new Ingrediente();
                var Table = ReferenciaTabla("Ingredientes");

                TableOperation retrieveOperation = TableOperation.Retrieve<IngredienteEntity>(PK, RK);

                var Res = await CacharIngrediente();
                
                async Task<Ingrediente> CacharIngrediente(){
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
                    return IngredienteFin;
                }
               return Res;   
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