using System;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage.Queue;
using System.Collections.Generic;

namespace AutNutriYA{
    public class RecordatorioRepo{
        public const string STORAGEACCOUNTNAME = "s101moyag8";
        public const string ACCOUNTKEY = "WPB64UdtcYgJZ+d+EQW8v+LPrj0YkakcAsQXtE6KvOhMaTxuIaP+EqD7tXHpG3hqoKMlAWFwdLR2e1vWU57i+g==";
    
        public List<Recordatorio> LeerRecordatorio(){
            var Table = ReferenciaTabla("Recordatorios");
            List<Recordatorio> Recordatorios = new List<Recordatorio>();
            TableQuery<RecordatorioEntity> query = new TableQuery<RecordatorioEntity>().Where
                (
                TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.NotEqual," ")
                );            
                
                CacharRecordatorios();

                async void CacharRecordatorios(){
                var list = new List<RecordatorioEntity>();
                var tk = new TableContinuationToken();
                foreach (RecordatorioEntity entity in await Table.ExecuteQuerySegmentedAsync(query,tk)){
                    
                    Recordatorios.Add(new Recordatorio(
                        entity.PartitionKey, 
                        entity.RowKey,
                        entity.platilloDesayuno,
                        entity.porcionDes,
                        entity.platilloComida,
                        entity.porcionCom,
                        entity.platilloCena,
                        entity.porcionCena,
                        entity.comentario));
                }
            }

            
            return Recordatorios;
        }
        
        public bool CrearRecordatorio(Recordatorio model){
            var Table = ReferenciaTabla("Recordatorios");

            Table.ExecuteAsync(TableOperation.Insert(new RecordatorioEntity(model.Nombre, model.Dia, model.platilloDesayuno,model.porcionDes, model.platilloComida,model.porcionCom, model.platilloCena,model.porcionCena, model.comentario)));

            return true;
        }

        /*public bool ActualizarRecordatorio(Recordatorio recordatorio)
        {
            var Table = ReferenciaTabla("Recordatorios");

            TableOperation retrieveOperation = TableOperation.Retrieve<RecordatorioEntity>(recordatorio.tipo, recordatorio.platillo);

            EditarRecordatorio();
                async void EditarRecordatorio(){
                    TableResult retrievedResult = await Table.ExecuteAsync(retrieveOperation);
                    RecordatorioEntity editEntity = (RecordatorioEntity)retrievedResult.Result;
                    if (editEntity != null)
                    {
                        editEntity.Ingredientes = recordatorio.ingredientes;
                        editEntity.Calorias     = recordatorio.calorias;
                        editEntity.porcion      = recordatorio.porcion;

                        TableOperation editOperation = TableOperation.Replace(editEntity);

                        await Table.ExecuteAsync(editOperation);
                    }

                }

            return true;
        }*/
        
        public bool BorrarRecordatorio(Recordatorio recordatorio){
            var Table = ReferenciaTabla("Recordatorios");
            TableOperation retrieveOperation = TableOperation.Retrieve<RecordatorioEntity>(recordatorio.Nombre, recordatorio.Dia);
            EliminarRecordatorio();
            async void EliminarRecordatorio(){
                TableResult retrievedResult = await Table.ExecuteAsync(retrieveOperation);
                RecordatorioEntity deleteEntity = (RecordatorioEntity)retrievedResult.Result;
                if(deleteEntity != null){
                    TableOperation deleteOperation = TableOperation.Delete(deleteEntity);
                    await Table.ExecuteAsync(deleteOperation);
                }
            }
            return true;

        }

        public Recordatorio LeerPorPKRK(string PK, string RK){
                
            Recordatorio RecordatorioFin = new Recordatorio();
            var Table = ReferenciaTabla("Recordatorios");

            TableOperation retrieveOperation = TableOperation.Retrieve<RecordatorioEntity>(PK, RK);

            CacharRecordatorio();
             System.Threading.Thread.Sleep(500);
                
            async void CacharRecordatorio(){
                TableResult retrievedResult = await Table.ExecuteAsync(retrieveOperation);
                var TempRec = (RecordatorioEntity)retrievedResult.Result;

                var RecordatorioTmp = new Recordatorio(
                    TempRec.PartitionKey, 
                    TempRec.RowKey,
                    TempRec.platilloDesayuno,
                    TempRec.porcionDes,
                    TempRec.platilloComida,
                    TempRec.porcionCom,
                    TempRec.platilloCena,
                    TempRec.porcionCena,
                    TempRec.comentario);
                RecordatorioFin = RecordatorioTmp;
            }
            System.Threading.Thread.Sleep(200);
            return RecordatorioFin;   
        }

        public CloudTable ReferenciaTabla(string nombreTabla){
            
            var StorageAccount = new CloudStorageAccount(new StorageCredentials(STORAGEACCOUNTNAME,ACCOUNTKEY),false);
            var tableClient = StorageAccount.CreateCloudTableClient();
            var Table = tableClient.GetTableReference(nombreTabla);

            return Table;
        }
    }

    public class RecordatorioEntity : TableEntity
    {
        public RecordatorioEntity() {}

        public RecordatorioEntity(string Nombre, string Dia, string platilloDesayuno, double porcionDes, string platilloComida, double porcionCom, string platilloCena, double porcionCena, string comentario)
        {
                
            PartitionKey = Nombre;
            RowKey = Dia;
            this.platilloDesayuno = platilloDesayuno;
            this.porcionDes = porcionDes;
            this.platilloComida = platilloComida;
            this.porcionCom = porcionCom;
            this.platilloCena = platilloCena;
            this.porcionCena = porcionCena;
            this.comentario =  comentario;
        }
        public string Nombre => PartitionKey;
        public string Dia => RowKey;
        public string platilloDesayuno{get; set;}
        public double porcionDes{get; set;}
        public string platilloComida{get; set;}
        public double porcionCom{get; set;}
        public string platilloCena{get; set;}
        public double porcionCena{get; set;}
        public string comentario{get; set;}

        public override string ToString() => $"{Nombre}{Dia}{platilloDesayuno}{porcionDes}{platilloComida}{porcionCom}{platilloCena}{porcionCena}{comentario}";

    }
}


