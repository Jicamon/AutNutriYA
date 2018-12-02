using System;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage.Queue;
using System.Collections.Generic;

namespace AutNutriYA{
    public class PlatillosRepo{
        public const string STORAGEACCOUNTNAME = "s101moyag8";
        public const string ACCOUNTKEY = "WPB64UdtcYgJZ+d+EQW8v+LPrj0YkakcAsQXtE6KvOhMaTxuIaP+EqD7tXHpG3hqoKMlAWFwdLR2e1vWU57i+g==";
    
        public List<Platillo> LeerPlatillo(){
            var Table = ReferenciaTabla("Platillos");
            List<Platillo> Platillos = new List<Platillo>();
            TableQuery<PlatilloEntity> query = new TableQuery<PlatilloEntity>().Where
                (
                TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.NotEqual," ")
                );            
                
                CacharPlatillos();

                async void CacharPlatillos(){
                var list = new List<PlatilloEntity>();
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
            }

            
            return Platillos;
        }
        
        public List<Platillo> LeerBebidas(){
            var Table = ReferenciaTabla("Platillos");
            List<Platillo> Bebidas = new List<Platillo>();
            TableQuery<PlatilloEntity> query = new TableQuery<PlatilloEntity>().Where
                (
                TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal,"Bebida")
                );            
                
                CacharBebidas();

                async void CacharBebidas(){
                var tk = new TableContinuationToken();
                foreach (PlatilloEntity entity in await Table.ExecuteQuerySegmentedAsync(query,tk)){
                    
                    Bebidas.Add(new Platillo(
                        entity.PartitionKey, 
                        entity.RowKey,
                        entity.Calorias, 
                        entity.Ingredientes,
                        entity.porcion
                    ));
                }
            }

            
            return Bebidas;
        }

        public List<Platillo> LeerAlimentos(){
            var Table = ReferenciaTabla("Platillos");
            List<Platillo> Alimentos = new List<Platillo>();
            TableQuery<PlatilloEntity> query = new TableQuery<PlatilloEntity>().Where
                (
                TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.NotEqual,"Bebida")
                );            
                
                CacharAlimentos();

                async void CacharAlimentos(){
                var tk = new TableContinuationToken();
                foreach (PlatilloEntity entity in await Table.ExecuteQuerySegmentedAsync(query,tk)){
                    
                    Alimentos.Add(new Platillo(
                        entity.PartitionKey, 
                        entity.RowKey,
                        entity.Calorias, 
                        entity.Ingredientes,
                        entity.porcion
                    ));
                }
            }

            
            return Alimentos;
        }

        public bool CrearPlatillo(Platillo model){
            var Table = ReferenciaTabla("Platillos");

            Table.ExecuteAsync(TableOperation.Insert(new PlatilloEntity(model.tipo,model.platillo,model.calorias,model.ingredientes,model.porcion)));

            return true;
        }

        public bool ActualizarPlatillo(Platillo platillo)
        {
            var Table = ReferenciaTabla("Platillos");

            TableOperation retrieveOperation = TableOperation.Retrieve<PlatilloEntity>(platillo.tipo, platillo.platillo);

            EditarPlatillo();
                async void EditarPlatillo(){
                    TableResult retrievedResult = await Table.ExecuteAsync(retrieveOperation);
                    PlatilloEntity editEntity = (PlatilloEntity)retrievedResult.Result;
                    if (editEntity != null)
                    {
                        editEntity.Ingredientes = platillo.ingredientes;
                        editEntity.Calorias     = platillo.calorias;
                        editEntity.porcion      = platillo.porcion;

                        TableOperation editOperation = TableOperation.Replace(editEntity);

                        await Table.ExecuteAsync(editOperation);
                    }

                }

            return true;
        }
        
        public bool BorrarPlatillo(Platillo platillo){
            var Table = ReferenciaTabla("Platillos");
            TableOperation retrieveOperation = TableOperation.Retrieve<PlatilloEntity>(platillo.tipo, platillo.platillo);
            EliminarPlatillo();
            async void EliminarPlatillo(){
                TableResult retrievedResult = await Table.ExecuteAsync(retrieveOperation);
                PlatilloEntity deleteEntity = (PlatilloEntity)retrievedResult.Result;
                if(deleteEntity != null){
                    TableOperation deleteOperation = TableOperation.Delete(deleteEntity);
                    await Table.ExecuteAsync(deleteOperation);
                }
            }
            return true;

        }

        public Platillo LeerPorPKRK(string PK, string RK)
            {
                
                Platillo PlatilloFin = new Platillo();
                var Table = ReferenciaTabla("Platillos");

                TableOperation retrieveOperation = TableOperation.Retrieve<PlatilloEntity>(PK, RK);

                CacharPlatillo();
                
                async void CacharPlatillo(){
                    TableResult retrievedResult = await Table.ExecuteAsync(retrieveOperation);
                    var TempPac = (PlatilloEntity)retrievedResult.Result;

                    var PlatilloTmp = new Platillo(
                        TempPac.PartitionKey,
                        TempPac.RowKey,
                        TempPac.Calorias,
                        TempPac.Ingredientes,
                        TempPac.porcion);
                    PlatilloFin = PlatilloTmp;
                }
               System.Threading.Thread.Sleep(200);
               return PlatilloFin;   
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