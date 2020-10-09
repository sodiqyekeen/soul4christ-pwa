using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using Grpc.Auth;
using Grpc.Core;
using Newtonsoft.Json;
using YourSoul4Christ.App.Models;

namespace YourSoul4Christ.App.Client
{
    public class FirebaseClient
    {
        private  FirestoreDb firestoreDb;
        private readonly HttpClient httpClient;

        public FirebaseClient(HttpClient _httpClient)
        {
            //string jsonCredential = "{\n  \"type\": \"service_account\",\n  \"project_id\": \"soul4christ\",\n  \"private_key_id\": \"6a6f3f371bd90813baafcfe57fd240534d6a19cb\",\n  \"private_key\": \"-----BEGIN PRIVATE KEY-----\\nMIIEvgIBADANBgkqhkiG9w0BAQEFAASCBKgwggSkAgEAAoIBAQCT8Oxt6OhsB8K4\\n9bbGbjj46ZOHbRs+IE2cGiAJ6JTz18zojAbk953nia9im93DtdlAMzOiqo6hGRfk\\neZVlApZeIr9DsWdxIxfAbpM55Q3yThqogXljeNkBfFyI3lerkerHuUxoSUWRDQyT\\nxzjiIl1WaO1alb89MLmetbch08QSwML57rPQbQemnM6TKwwN6l9iEtFzwjE6TSre\\nA1LxrZGKmyRWWobalnjv309IjsKTE7k50oinf7Sxf9orcX1N66LG+vmXYGibQkkY\\n6orV4xLhcWnatohxOV2rPTpYfga6CkqYW2/hK/CKzNUw2ndGhovqbsybSUB2kfDc\\n65JXSfcBAgMBAAECggEAFS92HBMzXW0LFIOTZf0YyYRhbQ5RzWbA1m7jUaft1LF9\\nlXuf7SAMi8Cqzfs2GbWr48axSUUVYbp/Mme63hktwVNGgoIgJxJ4b0Umbi7//Uf0\\nB1drM7R1lwHhGnSEp3wg2/MMHw8HGEt/FpZ77d4v8ZTbEJLLLQ8SNckd7Yku1eRv\\nCGsphg0hmg941khB3pN/pW3k+wE3zgRhjOK1a2jsKSxGcUplRpobjxXlm88mWgMG\\nnRjnLTI7FAtW+6jZF1HVR/GE+HG5IOYJyCxHaHO6nKZE5yxdrfnivHxRRBTm+AzT\\n3ywEO5b90vs4x+gJHkyx+FU+dC+7reyRYtcShQ1o9QKBgQDGCAjlY3iMTcDBpuRV\\nZpSkw/3+j8qcxy+HIkgAcGRV9rGQbSPX3RBIXAyremNgxMACrtY3z+QngwtDRCix\\nYhKcuJPoM+DF13XgJ9LaEO35gZizXGkY/EdyqzgcKN1E3GoWBerI9a0eV45zPgKM\\nMESKV5fIHE2WKdcVUyvMOvJM2wKBgQC/P0BlkJbD+2eT+xzWmailj6n+X6pcvhHJ\\nH6R0kkxFsfXc+OAWzw9dQLkP1SG/qeax4AXh+8Q9/LeOHr6IioYAanhITKX48euY\\nzuKCu28OLiZpXhuMeIe79qKSIdIn1KE3faFb1eKsSPuPHeR3Hu/rqz9cLoJWmB5N\\nmgxw+VjkUwKBgBc8lh6kf16nOPkAJkbHFKYIwWL/aFzhGniW0zAKABv+KHOz+sK3\\nk9OJyYrNf5+5NnaaIVTHNhRU0huAge3efiSZm/FhNPzB/Xjh82HXHrVqOYrCSyq9\\nX3zbDmhvexc7bc9LvnL6MOggifyHyDC+8Svyf0Lh31DBdBVyfu8l84yFAoGBAI8e\\n8b1QQFO4T+JhmUpMQfZslgYkZ6TyJPNDt9n5JhU+QcW1hlXrbQ8XgacElDYQGvHg\\nueZNeKX++wrjnSknqXKkOxG4MrkjqnF32fGF/W06Qkq2P52XEdUrlKwRnWlraZ7S\\naLAGV5UhJQkMkM5Im7ndNRRyRd4yBvOCJm9hWxG7AoGBALmctKOQhIGMzhnUc+ex\\nc8mEQ3gNY843VjMZSEaAjFdYEYjNsdUANH819HOu4de/hxEAv9DuA1fP/bDs4zuB\\nz/IWiVcvbeVsInknltTNI5qureL5xMpYtd/jz4D2cmzsibJ5Jfw4h578+VbvveSV\\nCLTYENvFTQZGGrbVUsBDiN9u\\n-----END PRIVATE KEY-----\\n\",\n  \"client_email\": \"soul4christ@appspot.gserviceaccount.com\",\n  \"client_id\": \"112682613101948630517\",\n  \"auth_uri\": \"https://accounts.google.com/o/oauth2/auth\",\n  \"token_uri\": \"https://oauth2.googleapis.com/token\",\n  \"auth_provider_x509_cert_url\": \"https://www.googleapis.com/oauth2/v1/certs\",\n  \"client_x509_cert_url\": \"https://www.googleapis.com/robot/v1/metadata/x509/soul4christ%40appspot.gserviceaccount.com\"\n}\n";
            //var credential = GoogleCredential.FromJson(jsonCredential);
            //Channel channel = new Channel(FirestoreClient.DefaultEndpoint.ToString(), credential.ToChannelCredentials());
            //FirestoreClient firestoreClient = FirestoreClient.Create(channel);
            httpClient = _httpClient;
           
            string keyPath = "C:\\Users\\Zach\\Documents\\Dev\\Demo\\yoursoul4christ\\YourSoul4Christ.App\\YourSoul4Christ.App.Client\\wwwroot\\private\\soul4christ-6a6f3f371bd9.json";
            // string keyPath = "private/soul4christ-6a6f3f371bd9.json";
            string key = httpClient.GetStringAsync("private/soul4christ-6a6f3f371bd9.json").GetAwaiter().GetResult();
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", key);
            firestoreDb = FirestoreDb.Create("soul4christ");
        }

        //private async Task<string> GetCredential()
        //{
        //    var key = await httpClient.GetStringAsync("private/soul4christ-6a6f3f371bd9.json");
        //}
       
        private void Test()
        {
            firestoreDb = FirestoreDb.Create("soul4christ");
        }

        public async Task<IEnumerable<Verse>> GetVersesFromStore()
        {
            Query verseQuery = firestoreDb.Collection("verese");
            QuerySnapshot documentSnapshots = await verseQuery.GetSnapshotAsync();

            List<Verse> verses = new List<Verse>();

            foreach (DocumentSnapshot document in documentSnapshots.Documents)
            {
                if (document.Exists)
                {
                    Dictionary<string, object> dictionary = document.ToDictionary();
                    string json = JsonConvert.SerializeObject(dictionary);
                    Verse verse = JsonConvert.DeserializeObject<Verse>(json);
                    verse.VerseId = document.Id;
                    verses.Add(verse);
                }
            }

            return verses;
        }

    }
}
