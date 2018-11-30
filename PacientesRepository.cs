using System;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage.Queue;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace AutNutriYA{
    public class PacientesRepository : Controller {

        
        public const string STORAGEACCOUNTNAME = "s101moyag8";
        public const string ACCOUNTKEY = "WPB64UdtcYgJZ+d+EQW8v+LPrj0YkakcAsQXtE6KvOhMaTxuIaP+EqD7tXHpG3hqoKMlAWFwdLR2e1vWU57i+g==";

        //private readonly UserManager<IdentityUser> userManager;
        
        public List<Paciente> LeerPaciente(){
            var Table = ReferenciaTabla("Pacientes");
            List<Paciente> Pacientes = new List<Paciente>();
            TableQuery<PacienteEntity> query = new TableQuery<PacienteEntity>().Where
                (
                TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.NotEqual    , " ")
                );            
                
                CacharPacientes();
                
               
                
                async void CacharPacientes(){
                var list = new List<PacienteEntity>();
                var tk = new TableContinuationToken();
                foreach (PacienteEntity entity in await Table.ExecuteQuerySegmentedAsync(query,tk)){
                    
                    Pacientes.Add(new Paciente(
                        entity.PartitionKey, 
                        entity.RowKey,
                        entity.NombrePac, 
                        entity.Edad,
                        entity.Altura,
                        entity.Peso,
                        entity.IMC,
                        entity.Alergias,
                        entity.CorreoNut));
                }
            }

            
            return Pacientes;
        }

        public void AgregarPacienteANutriologo(Paciente model)
        {
            var Table = ReferenciaTabla("Nutriologos");
            var entity = new DynamicTableEntity("¨PartitionKey", "RowKey");
            entity.ETag="*";
            Nutriologo nutriologo= new Nutriologo();
            TableOperation retrieveOperation = TableOperation.Retrieve<NutriologoEntity>(model.CorreoNut, model.NombreNut);

            CacharNutriologo();
                
                async void CacharNutriologo(){

                    TableResult retrievedResult = await Table.ExecuteAsync(retrieveOperation);
                    var newNutriologo = (NutriologoEntity)retrievedResult.Result;
                    System.Threading.Thread.Sleep(500);
                    Nutriologo newNutrilogo2 = new Nutriologo(
                        newNutriologo.PartitionKey, 
                        newNutriologo.RowKey,
                        newNutriologo.Pacientes, 
                        newNutriologo.Direccion,
                        newNutriologo.Telefono);

                    nutriologo = newNutrilogo2;
                } 

            entity.Properties.Add("Pacientes",new EntityProperty("Algo"));
            var mergeOperation = TableOperation.Merge(entity);
            Table.ExecuteAsync(mergeOperation);
        }

        public bool CrearPaciente(Paciente model, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager){
            var Table = ReferenciaTabla("Pacientes");

            crearPacienteLOGIN(userManager ,model);
            Table.ExecuteAsync(TableOperation.Insert(new PacienteEntity(model.NombreNut,model.Correo,model.NombrePac,model.Edad,model.Altura,model.Peso,model.CorreoNut)));

            return true;
        }
        public void crearPacienteLOGIN(UserManager<IdentityUser> uManager, Paciente model){

            if (uManager.FindByEmailAsync(model.Correo).Result == null){

                IdentityUser user = new IdentityUser
                {
                    UserName = model.Correo,
                    Email = model.Correo
                };

                IdentityResult result = uManager.CreateAsync(user, "Contraseña12!").Result;

                if(result.Succeeded)
                {
                    uManager.AddToRoleAsync(user, "Paciente").Wait();
                }

            }

        }


        public async void BorrarPaciente(string PK, string RK){
            var Table = ReferenciaTabla("Pacientes");

            TableOperation retrieveOperation = TableOperation.Retrieve<PacienteEntity>(PK, RK);
            TableResult retrievedResult = await Table.ExecuteAsync(retrieveOperation);
            PacienteEntity deleteEntity = (PacienteEntity)retrievedResult.Result;

            if (deleteEntity != null)
            {
                TableOperation deleteOperation = TableOperation.Delete(deleteEntity);

                await Table.ExecuteAsync(deleteOperation);
            }
        }
        

        public bool ActualizarPaciente(Paciente Paci)
        {

            var Table = ReferenciaTabla("Pacientes");

            TableOperation retrieveOperation = TableOperation.Retrieve<PacienteEntity>(Paci.NombreNut,Paci.Correo);
            System.Threading.Thread.Sleep(2000);
            EditarPaciente();
                async void EditarPaciente(){
                    TableResult retrievedResult = await Table.ExecuteAsync(retrieveOperation);
                    PacienteEntity editEntity = (PacienteEntity)retrievedResult.Result;
                    if (editEntity != null)
                    {
                        
                        editEntity.NombrePac = Paci.NombrePac;
                        editEntity.Edad      = Paci.Edad;
                        editEntity.Altura    = Paci.Altura;
                        editEntity.Peso      = Paci.Peso;

                        TableOperation editOperation = TableOperation.Replace(editEntity);

                        await Table.ExecuteAsync(editOperation);
                    }

                }

            return true;
        }

        public bool BorrarPaciente(Paciente Paci){
            var Table = ReferenciaTabla("Pacientes");
            TableOperation retrieveOperation = TableOperation.Retrieve<PacienteEntity>(Paci.NombreNut, Paci.Correo);
            EliminarPaciente();
            async void EliminarPaciente(){
                TableResult retrievedResult = await Table.ExecuteAsync(retrieveOperation);
                PacienteEntity deleteEntity = (PacienteEntity)retrievedResult.Result;
                if(deleteEntity != null){
                    TableOperation deleteOperation = TableOperation.Delete(deleteEntity);
                    await Table.ExecuteAsync(deleteOperation);
                }
            }
            System.Threading.Thread.Sleep(800);
            return true;

        }

        public PacienteEntity PacEntTemp = new PacienteEntity();
        public Paciente LeerPorPKRK(string PK, string RK)
            {
                
                Paciente PacienteFin = new Paciente();
                var Table = ReferenciaTabla("Pacientes");

                TableOperation retrieveOperation = TableOperation.Retrieve<PacienteEntity>(PK, RK);

                CacharPaciente();
                
                async void CacharPaciente(){
                    TableResult retrievedResult = await Table.ExecuteAsync(retrieveOperation);
                    var TempPac = (PacienteEntity)retrievedResult.Result;

                    var PaciFin = new Paciente(
                        TempPac.PartitionKey,
                        TempPac.RowKey,
                        TempPac.NombrePac,
                        TempPac.Edad,
                        TempPac.Altura,
                        TempPac.Peso,
                        TempPac.IMC,
                        TempPac.Alergias,
                        TempPac.CorreoNut);
                    PacienteFin = PaciFin;
                }
               System.Threading.Thread.Sleep(500);
               return PacienteFin;   
            }

        
        public CloudTable ReferenciaTabla(string nombreTabla){
            
            var StorageAccount = new CloudStorageAccount(new StorageCredentials(STORAGEACCOUNTNAME,ACCOUNTKEY),false);
            var tableClient = StorageAccount.CreateCloudTableClient();
            var Table = tableClient.GetTableReference(nombreTabla);

            return Table;
        }
    }

    public class PacienteEntity : TableEntity
    {
        public PacienteEntity() { }
        public string NombreNut => PartitionKey;
        public string Correo => RowKey;
        public string NombrePac {get; set;}
        public int Edad { get; set; }
        public int Altura { get; set; }
        public int Peso { get; set; }
        public double IMC { get; set; }
        public string Alergias { get; set; }
        public string CorreoNut { get; set; }

        public override string ToString() => $"{NombreNut} {Correo} {NombrePac} {Edad} {Altura} {Peso} {IMC} {Alergias} {CorreoNut}";

        public PacienteEntity(string nombreNut,string correo,string nombrePac, int edad, int altura, int peso, string correoNut)
        {

            PartitionKey = nombreNut;
            RowKey = correo;
            NombrePac = nombrePac;
            Edad = edad;
            Altura = altura;
            Peso = peso;
            var Wea = (double)Altura / 100;
            Alergias = "";
            IMC = (double)Peso / (Wea*Wea);
            CorreoNut = correoNut;

        }
    }
}