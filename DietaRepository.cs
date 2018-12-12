using System;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage.Queue;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutNutriYA{
    public class DietasRepo{
        public const string STORAGEACCOUNTNAME = "s101moyag8";
        public const string ACCOUNTKEY = "WPB64UdtcYgJZ+d+EQW8v+LPrj0YkakcAsQXtE6KvOhMaTxuIaP+EqD7tXHpG3hqoKMlAWFwdLR2e1vWU57i+g==";
    
        public async Task<List<Dieta>> LeerDieta(){
            var Table = ReferenciaTabla("Dietas");
            List<Dieta> Dietas = new List<Dieta>();
            TableQuery<DietaEntity> query = new TableQuery<DietaEntity>().Where
                (
                TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.NotEqual," ")
                );            
                
                var Res = await CacharDietas();

            async Task<List<Dieta>> CacharDietas(){
            var tk = new TableContinuationToken();
                foreach (DietaEntity entity in await Table.ExecuteQuerySegmentedAsync(query,tk)){
                    
                    Dietas.Add(new Dieta(
                        entity.PartitionKey, 
                        entity.RowKey,
                        entity.Desayuno,
                        entity.Desayuno_A,
                        entity.ColacionM,
                        entity.ColacionM_A,
                        entity.Comida,
                        entity.Comida_A,
                        entity.ColacionT,
                        entity.ColacionT_A,
                        entity.Cena,
                        entity.Cena_A,
                        entity.Bebida1,
                        entity.Bebida1_A,
                        entity.Bebida2,
                        entity.Bebida2_A,
                        entity.Bebida3,
                        entity.Bebida3_A));
                }
                return Dietas;
            }
            return Res;
        }
        
        public bool CrearDieta(Dieta model){
            var Table = ReferenciaTabla("Dietas");

            Table.ExecuteAsync(TableOperation.Insert(new DietaEntity(model.Nombre,model.Dia,model.Desayuno,model.Desayuno_A,
                                                    model.ColacionM,model.ColacionM_A,model.Comida,model.Comida_A,
                                                    model.ColacionT,model.ColacionT_A,model.Cena, model.Cena_A, 
                                                    model.Bebida1,model.Bebida1_A, model.Bebida2,model.Bebida2_A, 
                                                    model.Bebida3,model.Bebida3_A)));

            return true;
        }

        public async Task<bool> ActualizarDieta(Dieta dieta)
        {
            var Table = ReferenciaTabla("Dietas");

            TableOperation retrieveOperation = TableOperation.Retrieve<DietaEntity>(dieta.Nombre, dieta.Dia);
            
            var funciono = await EditarDieta();
                async Task<bool> EditarDieta(){
                    TableResult retrievedResult = await Table.ExecuteAsync(retrieveOperation);
                    DietaEntity editEntity = (DietaEntity)retrievedResult.Result;
                    if (editEntity != null)
                    {
                        editEntity.Desayuno    = dieta.Desayuno;
                        editEntity.ColacionM   = dieta.ColacionM;
                        editEntity.Comida      = dieta.Comida;
                        editEntity.ColacionT   = dieta.ColacionT;
                        editEntity.Cena        = dieta.Cena;
                        editEntity.Bebida1     = dieta.Bebida1;
                        editEntity.Bebida2     = dieta.Bebida2;
                        editEntity.Bebida3     = dieta.Bebida3;
                        editEntity.Desayuno_A  = dieta.Desayuno_A;
                        editEntity.ColacionM_A = dieta.ColacionM_A;
                        editEntity.Comida_A    = dieta.Comida_A;
                        editEntity.ColacionT_A = dieta.ColacionT_A;
                        editEntity.Cena_A      = dieta.Cena_A;
                        editEntity.Bebida1_A   = dieta.Bebida1_A;
                        editEntity.Bebida2_A   = dieta.Bebida2_A;
                        editEntity.Bebida3_A   = dieta.Bebida3_A;

                        TableOperation editOperation = TableOperation.Replace(editEntity);

                        await Table.ExecuteAsync(editOperation);
                        return true;
                    }else return false;
                }

            return funciono;
        }
        
        public async Task<bool> BorrarDieta(Dieta dieta){
            var Table = ReferenciaTabla("Dietas");
            TableOperation retrieveOperation = TableOperation.Retrieve<DietaEntity>(dieta.Nombre, dieta.Dia);
            var funciono = await EliminarDieta();
            async Task<bool> EliminarDieta(){
                TableResult retrievedResult = await Table.ExecuteAsync(retrieveOperation);
                DietaEntity deleteEntity = (DietaEntity)retrievedResult.Result;
                if(deleteEntity != null){
                    TableOperation deleteOperation = TableOperation.Delete(deleteEntity);
                    await Table.ExecuteAsync(deleteOperation);
                    return true;
                }else return false;
            }
            return true;

        }

        public async Task<List<Dieta>> LeerDieta(string correo){
            Dieta dieta = new Dieta();
            var list = new List<Dieta>();
            DietaEntity dieta2 = new DietaEntity();
            var Table = ReferenciaTabla("Dietas");
            TableQuery<DietaEntity> query = new TableQuery<DietaEntity>().Where(
                TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, correo));
            var Res = await CacharDieta();

            async Task<List<Dieta>> CacharDieta(){
                var tk = new TableContinuationToken();
                foreach (DietaEntity entity in await Table.ExecuteQuerySegmentedAsync(query, tk))
                {
                    var newDieta2 = new Dieta(
                        entity.PartitionKey, 
                        entity.RowKey,
                        entity.Desayuno,
                        entity.Desayuno_A,
                        entity.ColacionM,
                        entity.ColacionM_A,
                        entity.Comida,
                        entity.Comida_A,
                        entity.ColacionT,
                        entity.ColacionT_A,
                        entity.Cena,
                        entity.Cena_A,
                        entity.Bebida1,
                        entity.Bebida1_A,
                        entity.Bebida2,
                        entity.Bebida2_A,
                        entity.Bebida3,
                        entity.Bebida3_A);

                    list.Add(newDieta2);
                    

                }
                return list;
            }
            return Res;
                       
        }
        public async Task<Dieta> LeerPorPKRK(string PK, string RK)
            {
                
                Dieta DietaFin = new Dieta();
                var Table = ReferenciaTabla("Dietas");

                TableOperation retrieveOperation = TableOperation.Retrieve<DietaEntity>(PK, RK);

                var Res = await CacharDieta();
                
                async Task<Dieta> CacharDieta(){
                    TableResult retrievedResult = await Table.ExecuteAsync(retrieveOperation);
                    var TempD = (DietaEntity)retrievedResult.Result;

                    var DietaTmp = new Dieta(
                        TempD.PartitionKey,
                        TempD.RowKey,
                        TempD.Desayuno,
                        TempD.Desayuno_A,
                        TempD.ColacionM,
                        TempD.ColacionM_A,
                        TempD.Comida,
                        TempD.Comida_A,
                        TempD.ColacionT,
                        TempD.ColacionT_A,
                        TempD.Cena,
                        TempD.Cena_A,
                        TempD.Bebida1,
                        TempD.Bebida1_A,
                        TempD.Bebida2,
                        TempD.Bebida2_A,
                        TempD.Bebida3,
                        TempD.Bebida3_A);
                    DietaFin = DietaTmp;

                    return DietaFin;
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

    public class DietaEntity : TableEntity
        {
            public DietaEntity() {}

            public DietaEntity(string Nombre, string Dia, string Desayuno, string Desayuno_A, string ColacionM, string ColacionM_A, 
                            string Comida, string Comida_A, string ColacionT, string ColacionT_A, string Cena, string Cena_A, 
                            string Bebida1, string Bebida1_A, string Bebida2, string Bebida2_A, string Bebida3, string Bebida3_A){
                
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
                this.Desayuno_A = Desayuno_A;
                this.ColacionM_A = ColacionM_A;
                this.Comida_A = Comida_A;
                this.ColacionT_A = ColacionT_A;
                this.Cena_A = Cena_A;
                this.Bebida1_A=Bebida1_A;
                this.Bebida2_A=Bebida2_A;
                this.Bebida3_A=Bebida3_A;
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
            public string Desayuno_A{get; set;}
            public string ColacionM_A{get; set;}
            public string Comida_A{get; set;}
            public string ColacionT_A{get; set;}
            public string Cena_A{get; set;}
            public string Bebida1_A { get; set; }
            public string Bebida2_A { get; set; }
            public string Bebida3_A { get; set; }

            public override string ToString() => $"{Nombre}{Dia}{Desayuno}{Desayuno_A}{ColacionM}{ColacionM_A}{Comida}{Comida_A}{ColacionT}{ColacionT_A}{Cena}{Cena_A}{Bebida1}{Bebida1_A}{Bebida2}{Bebida2_A}{Bebida3}{Bebida3_A}";

        }
    
        
}