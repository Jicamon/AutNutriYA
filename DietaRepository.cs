using System;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage.Queue;
using System.Collections.Generic;

namespace AutNutriYA{
    public class DietasRepo{
        public const string STORAGEACCOUNTNAME = "s101moyag8";
        public const string ACCOUNTKEY = "WPB64UdtcYgJZ+d+EQW8v+LPrj0YkakcAsQXtE6KvOhMaTxuIaP+EqD7tXHpG3hqoKMlAWFwdLR2e1vWU57i+g==";
    
        public List<Dieta> LeerDieta(){
            var Table = ReferenciaTabla("Dietas");
            List<Dieta> Dietas = new List<Dieta>();
            TableQuery<DietaEntity> query = new TableQuery<DietaEntity>().Where
                (
                TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.NotEqual," ")
                );            
                
                CacharDietas();

                async void CacharDietas(){
                var list = new List<DietaEntity>();
                var tk = new TableContinuationToken();
                foreach (DietaEntity entity in await Table.ExecuteQuerySegmentedAsync(query,tk)){
                    
                    Dietas.Add(new Dieta(
                        entity.PartitionKey, 
                        entity.RowKey,
                        entity.Desayuno,
                        entity.ColacionM,
                        entity.Comida,
                        entity.ColacionT,
                        entity.Cena,
                        entity.Bebida1,
                        entity.Bebida2,
                        entity.Bebida3));
                }
            }

            
            return Dietas;
        }
        
        public bool CrearDieta(Dieta model){
            var Table = ReferenciaTabla("Dietas");

            Table.ExecuteAsync(TableOperation.Insert(new DietaEntity(model.Nombre,model.Dia,model.Desayuno,model.ColacionM,model.Comida,model.ColacionT,model.Cena, model.Bebida1, model.Bebida2, model.Bebida3)));

            return true;
        }

        public bool ActualizarDieta(Dieta dieta)
        {
            var Table = ReferenciaTabla("Dietas");

            TableOperation retrieveOperation = TableOperation.Retrieve<DietaEntity>(dieta.Nombre, dieta.Dia);

            EditarDieta();
                async void EditarDieta(){
                    TableResult retrievedResult = await Table.ExecuteAsync(retrieveOperation);
                    DietaEntity editEntity = (DietaEntity)retrievedResult.Result;
                    if (editEntity != null)
                    {
                        editEntity.Desayuno   = dieta.Desayuno;
                        editEntity.ColacionM  = dieta.ColacionM;
                        editEntity.Comida     = dieta.Comida;
                        editEntity.ColacionT  = dieta.ColacionT;
                        editEntity.Cena       = dieta.Cena;
                        editEntity.Bebida1    = dieta.Bebida1;
                        editEntity.Bebida2    = dieta.Bebida2;
                        editEntity.Bebida3    = dieta.Bebida3;

                        TableOperation editOperation = TableOperation.Replace(editEntity);

                        await Table.ExecuteAsync(editOperation);
                    }

                }

            return true;
        }
        
        public bool BorrarDieta(Dieta dieta){
            var Table = ReferenciaTabla("Dietas");
            TableOperation retrieveOperation = TableOperation.Retrieve<DietaEntity>(dieta.Nombre, dieta.Dia);
            EliminarDieta();
            async void EliminarDieta(){
                TableResult retrievedResult = await Table.ExecuteAsync(retrieveOperation);
                DietaEntity deleteEntity = (DietaEntity)retrievedResult.Result;
                if(deleteEntity != null){
                    TableOperation deleteOperation = TableOperation.Delete(deleteEntity);
                    await Table.ExecuteAsync(deleteOperation);
                }
            }
            return true;

        }

        public List<Dieta> LeerDieta(string correo){
            Dieta dieta = new Dieta();
            var list = new List<Dieta>();
            DietaEntity dieta2 = new DietaEntity();
            var Table = ReferenciaTabla("Dietas");
            TableQuery<DietaEntity> query = new TableQuery<DietaEntity>().Where(
                TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, correo));
            System.Threading.Thread.Sleep(1000);
            CacharDieta();
            async void CacharDieta(){
                
                var tk = new TableContinuationToken();
                foreach (DietaEntity entity in await Table.ExecuteQuerySegmentedAsync(query, tk)){
                    
                    var newDieta2 = new Dieta(
                        entity.PartitionKey, 
                        entity.RowKey,
                        entity.Desayuno,
                        entity.ColacionM,
                        entity.Comida,
                        entity.ColacionT,
                        entity.Cena,
                        entity.Bebida1,
                        entity.Bebida2,
                        entity.Bebida3);

                    list.Add(newDieta2);
                    

                }
            }
            System.Threading.Thread.Sleep(500);
            return list;
            
            
        }
        public Dieta LeerPorPKRK(string PK, string RK)
            {
                
                Dieta DietaFin = new Dieta();
                var Table = ReferenciaTabla("Dietas");

                TableOperation retrieveOperation = TableOperation.Retrieve<DietaEntity>(PK, RK);

                CacharDieta();
                
                async void CacharDieta(){
                    TableResult retrievedResult = await Table.ExecuteAsync(retrieveOperation);
                    var TempD = (DietaEntity)retrievedResult.Result;

                    var DietaTmp = new Dieta(
                        TempD.PartitionKey,
                        TempD.RowKey,
                        TempD.Desayuno,
                        TempD.ColacionM,
                        TempD.Comida,
                        TempD.ColacionT,
                        TempD.Cena,
                        TempD.Bebida1,
                        TempD.Bebida2,
                        TempD.Bebida3);
                    DietaFin = DietaTmp;
                }
               System.Threading.Thread.Sleep(200);
               return DietaFin;   
            }

        public CloudTable ReferenciaTabla(string nombreTabla){
            
            var StorageAccount = new CloudStorageAccount(new StorageCredentials(STORAGEACCOUNTNAME,ACCOUNTKEY),false);
            var tableClient = StorageAccount.CreateCloudTableClient();
            var Table = tableClient.GetTableReference(nombreTabla);

            return Table;
        }
    }

    public class DietaEntity : TableEntity
        {
            public DietaEntity() {}

            public DietaEntity(string Nombre, string Dia, string Desayuno, string ColacionM, string Comida, string ColacionT, string Cena, string Bebida1, string Bebida2, string Bebida3){
                
                PartitionKey = Nombre;
                RowKey = Dia;
                this.Desayuno = Desayuno;
                this.ColacionM = ColacionM;
                this.Comida = Comida;
                this.ColacionT = ColacionT;
                this.Cena = Cena;
                this.Bebida1=Bebida1;
                this.Bebida2=Bebida2;
                this.Bebida3=Bebida3;
            }

            public string Nombre => PartitionKey;
            public string Dia => RowKey;
            public string Desayuno{get; set;}
            public string ColacionM{get; set;}
            public string Comida{get; set;}
            public string ColacionT{get; set;}
            public string Cena{get; set;}
            public string Bebida1 { get; set; }
            public string Bebida2 { get; set; }
            public string Bebida3 { get; set; }

            public override string ToString() => $"{Nombre}{Dia}{Desayuno}{ColacionM}{Comida}{ColacionT}{Cena}{Bebida1}{Bebida2}{Bebida3}";

        }
    
        
}