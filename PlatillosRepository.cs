using System;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage.Queue;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutNutriYA{
    public class PlatillosRepo{
        public const string STORAGEACCOUNTNAME = "s101moyag8";
        public const string ACCOUNTKEY = "WPB64UdtcYgJZ+d+EQW8v+LPrj0YkakcAsQXtE6KvOhMaTxuIaP+EqD7tXHpG3hqoKMlAWFwdLR2e1vWU57i+g==";
    
        public async Task<List<Platillo>> LeerPlatillo(){
            var Table = ReferenciaTabla("Platillos");
            List<Platillo> Platillos = new List<Platillo>();
            TableQuery<PlatilloEntity> query = new TableQuery<PlatilloEntity>().Where
                (
                TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.NotEqual," ")
                );            
                
                var Res = await CacharPlatillos();

                async Task<List<Platillo>> CacharPlatillos(){
                var tk = new TableContinuationToken();
                foreach (PlatilloEntity entity in await Table.ExecuteQuerySegmentedAsync(query,tk)){
                    
                    Platillos.Add(new Platillo(
                        entity.PartitionKey, 
                        entity.RowKey,
                        entity.Calorias, 
                        entity.Ingredientes,
                        entity.porcion
                    ));
                }
                return Platillos;
            }

            
            return Res;
        }
        
        public bool CrearPlatillo(Platillo model){
            var Table = ReferenciaTabla("Platillos");

            Table.ExecuteAsync(TableOperation.Insert(new PlatilloEntity(model.tipo,model.platillo,model.calorias,model.ingredientes,model.porcion)));

            return true;
        }

        public async Task<bool> ActualizarPlatillo(Platillo platillo)
        {
            var Table = ReferenciaTabla("Platillos");

            TableOperation retrieveOperation = TableOperation.Retrieve<PlatilloEntity>(platillo.tipo, platillo.platillo);

            var funciono= await EditarPlatillo();
                async Task<bool> EditarPlatillo(){
                    TableResult retrievedResult = await Table.ExecuteAsync(retrieveOperation);
                    PlatilloEntity editEntity = (PlatilloEntity)retrievedResult.Result;
                    if (editEntity != null)
                    {
                        editEntity.Ingredientes = platillo.ingredientes;
                        editEntity.Calorias     = platillo.calorias;
                        editEntity.porcion      = platillo.porcion;

                        TableOperation editOperation = TableOperation.Replace(editEntity);

                        await Table.ExecuteAsync(editOperation);
                        return true;
                    }else return false;
                    
                }

            return funciono;
        }
        
        public async Task<bool> BorrarPlatillo(Platillo platillo){
            var Table = ReferenciaTabla("Platillos");
            TableOperation retrieveOperation = TableOperation.Retrieve<PlatilloEntity>(platillo.tipo, platillo.platillo);
            var funciono = await EliminarPlatillo();
            async Task<bool> EliminarPlatillo(){
                TableResult retrievedResult = await Table.ExecuteAsync(retrieveOperation);
                PlatilloEntity deleteEntity = (PlatilloEntity)retrievedResult.Result;
                if(deleteEntity != null){
                    TableOperation deleteOperation = TableOperation.Delete(deleteEntity);
                    await Table.ExecuteAsync(deleteOperation);
                    return true;
                } else return false;
            }
            return funciono;

        }

        public async Task<Platillo> LeerPorPKRK(string PK, string RK)
            {
                
                Platillo PlatilloFin = new Platillo();
                var Table = ReferenciaTabla("Platillos");

                TableOperation retrieveOperation = TableOperation.Retrieve<PlatilloEntity>(PK, RK);

                var Res = await CacharPlatillo();
                
                async Task<Platillo> CacharPlatillo(){
                    TableResult retrievedResult = await Table.ExecuteAsync(retrieveOperation);
                    var TempPac = (PlatilloEntity)retrievedResult.Result;

                    var PlatilloTmp = new Platillo(
                        TempPac.PartitionKey,
                        TempPac.RowKey,
                        TempPac.Calorias,
                        TempPac.Ingredientes,
                        TempPac.porcion);
                    PlatilloFin = PlatilloTmp;
                    return PlatilloFin;
                }
               return Res;   
            }

        public CloudTable ReferenciaTabla(string nombreTabla){
            
            var StorageAccount = new CloudStorageAccount(new StorageCredentials(STORAGEACCOUNTNAME,ACCOUNTKEY),false);
            var tableClient = StorageAccount.CreateCloudTableClient();
            var Table = tableClient.GetTableReference(nombreTabla);

            return Table;
        }
    }

    public class PlatilloEntity : TableEntity
    {
        public PlatilloEntity() { }

        public PlatilloEntity(string tipo,string Platillo, double Calorias, string Ingredientes, double porcion)
        {
                
            PartitionKey = tipo;
            RowKey = Platillo;
            this.Calorias = Calorias;
            this.Ingredientes = Ingredientes;
            this.porcion = porcion;
        }
        public string tipo => PartitionKey;
        public string Platillo => RowKey;
        public string Ingredientes { get; set; }
        public double Calorias { get; set; }
        public double porcion { get; set; }

        public override string ToString() => $"{tipo}{Platillo}{Calorias}{Ingredientes}{porcion}";

    }
    
        
}