using System;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage.Queue;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AutNutriYA{
    public class NutriologosRepository{
        public const string STORAGEACCOUNTNAME = "s101moyag8";
        public const string ACCOUNTKEY = "WPB64UdtcYgJZ+d+EQW8v+LPrj0YkakcAsQXtE6KvOhMaTxuIaP+EqD7tXHpG3hqoKMlAWFwdLR2e1vWU57i+g==";

        public async Task<List<Nutriologo>> LeerNutriologos(){
            var StorageAccount = new CloudStorageAccount(new StorageCredentials(STORAGEACCOUNTNAME,ACCOUNTKEY),false);
            var tableClient = StorageAccount.CreateCloudTableClient();
            var Table = tableClient.GetTableReference("Nutriologo");
            List<Nutriologo> Nutriologos = new List<Nutriologo>();
            TableQuery<NutriologoEntity> query = new TableQuery<NutriologoEntity>().Where
                (
                TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.NotEqual," ")
                );            
                
            var Res = await CacharNutriologos();

            async Task<List<Nutriologo>> CacharNutriologos(){
                var tk = new TableContinuationToken();
                foreach (NutriologoEntity entity in await Table.ExecuteQuerySegmentedAsync(query,tk)){
                    
                    Nutriologos.Add(new Nutriologo(
                        entity.PartitionKey, 
                        entity.RowKey,
                        entity.Pacientes, 
                        entity.Direccion,
                        entity.Telefono));
                }
                return Nutriologos;
            }

            
            return Res;
        }

        public async Task<Nutriologo> LeerNutriologo(string correo, string nombre){
                
            var Table = ReferenciaTabla("Nutriologo");
            Nutriologo nutriologo= new Nutriologo();
            TableOperation retrieveOperation = TableOperation.Retrieve<NutriologoEntity>(correo, nombre);

            var Res = await CacharNutriologo();
            async Task<Nutriologo> CacharNutriologo(){

                TableResult retrievedResult = await Table.ExecuteAsync(retrieveOperation);
                var newNutriologo = (NutriologoEntity)retrievedResult.Result;
                
                var newNutrilogo2 = new Nutriologo(
                        newNutriologo.PartitionKey, 
                        newNutriologo.RowKey,
                        newNutriologo.Pacientes, 
                        newNutriologo.Direccion,
                        newNutriologo.Telefono);

                nutriologo = newNutrilogo2;
                return nutriologo;
                }
            return Res;
        }

        public async Task<Nutriologo> LeerNutriologo(string correo){
            Nutriologo nutriologo = new Nutriologo();
            NutriologoEntity nutriologo2 = new NutriologoEntity();
            var Table = ReferenciaTabla("Nutriologo");
            TableQuery<NutriologoEntity> query = new TableQuery<NutriologoEntity>().Where(
                TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, correo));
            
            var res = await CacharNutriologo();

            async Task<Nutriologo> CacharNutriologo(){
                var list = new List<NutriologoEntity>();
                var tk = new TableContinuationToken();
                foreach (NutriologoEntity entity in await Table.ExecuteQuerySegmentedAsync(query, tk)){
                    
                    var newNutrilogo2 = new Nutriologo(
                        entity.PartitionKey, 
                        entity.RowKey,
                        entity.Pacientes, 
                        entity.Direccion,
                        entity.Telefono);

                    nutriologo = newNutrilogo2;
                    

                }
                return nutriologo;
            }
            return res;
            
            
        }

        public async Task<bool> ActualizarNutriologo(Nutriologo Nutriologo)
        {
            var Table = ReferenciaTabla("Nutriologo");

            TableOperation retrieveOperation = TableOperation.Retrieve<NutriologoEntity>(Nutriologo.Correo, Nutriologo.Nombre);

            var funciono = await EditarNutriologo();
            
            async Task<bool> EditarNutriologo(){
                TableResult retrievedResult = await Table.ExecuteAsync(retrieveOperation);
                NutriologoEntity editEntity = (NutriologoEntity)retrievedResult.Result;
                if (editEntity != null)
                {
                    editEntity.Pacientes = Nutriologo.Pacientes;
                    editEntity.Telefono  = Nutriologo.Telefono;
                    editEntity.Direccion = Nutriologo.Direccion;
                    editEntity.newNombre = Nutriologo.Nombre;
                    editEntity.newCorreo = Nutriologo.Correo;

                    TableOperation editOperation = TableOperation.Replace(editEntity);
                    
                    await Table.ExecuteAsync(editOperation);
                    return true;
                }else return false;
                
            }

            return funciono;
        }

        public async Task<bool> BorrarNutriologo(Nutriologo model)
        {
            var Table = ReferenciaTabla("Nutriologo");
            TableOperation retrieveOperation = TableOperation.Retrieve<NutriologoEntity>(model.Correo,model.Nombre);
            
            var funciono = await EliminarNutriologo();
            async Task<bool> EliminarNutriologo(){
                
                TableResult retrievedResult = await Table.ExecuteAsync(retrieveOperation);
                NutriologoEntity deleteEntity = (NutriologoEntity)retrievedResult.Result;
                if (deleteEntity != null){
                    TableOperation deleteOperation = TableOperation.Delete(deleteEntity);
                    // Execute the operation.
                    await Table.ExecuteAsync(deleteOperation);
                    return true;
                }else return false;
            
            }
            return funciono;

        }

        public bool CrearNutriologo(Nutriologo model, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, string contrasena){
            var StorageAccount = new CloudStorageAccount(new StorageCredentials(STORAGEACCOUNTNAME,ACCOUNTKEY),false);
            var tableClient = StorageAccount.CreateCloudTableClient();
            var Table = tableClient.GetTableReference("Nutriologo");
            crearNutriologoLOGIN(userManager, model, contrasena);
            Table.ExecuteAsync(TableOperation.Insert(new NutriologoEntity(model.Correo,model.Nombre,model.Telefono,model.Direccion)));

            return true;
        }
        public void crearNutriologoLOGIN(UserManager<IdentityUser> uManager, Nutriologo model, string contrasena){

            if (uManager.FindByEmailAsync(model.Correo).Result == null){

                IdentityUser user = new IdentityUser
                {
                    UserName = model.Correo,
                    Email = model.Correo
                };

                IdentityResult result = uManager.CreateAsync(user, contrasena).Result;

                if(result.Succeeded)
                {
                    uManager.AddToRoleAsync(user, "Nutriologo").Wait();
                }

            }

        }

        
        public CloudTable ReferenciaTabla(string nombreTabla){
            
            var StorageAccount = new CloudStorageAccount(new StorageCredentials(STORAGEACCOUNTNAME,ACCOUNTKEY),false);
            var tableClient = StorageAccount.CreateCloudTableClient();
            var Table = tableClient.GetTableReference(nombreTabla);

            return Table;
        }
    }

    public class NutriologoEntity : TableEntity
        {
            public NutriologoEntity() { }

            public NutriologoEntity(string newCorreo, string newNombre,double newTelefono,string newDireccion)
            {
                
                PartitionKey = newCorreo;
                RowKey = newNombre;
                Pacientes = "";
                Telefono = newTelefono;
                Direccion = newDireccion;
            }

            public string newCorreo { get; set; }
            public string newNombre { get; set; }
            public string Pacientes { get; set; }
            public double Telefono { get; set; }
            public string Direccion { get; set; }

            public override string ToString() => $"{newCorreo}{newNombre}{Pacientes}{Telefono}{Direccion}";

        }


}