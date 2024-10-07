using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

class Program
{
    static void Main()
    {
        // Carica il certificato
        X509Certificate2 myCertificate = new X509Certificate2(@"C:\Temp\KEYSTORE.p12", "AppTest", X509KeyStorageFlags.Exportable);

        // Estrai la chiave privata
        RSACryptoServiceProvider privateKey = myCertificate.PrivateKey as RSACryptoServiceProvider;
        
        // Usa RSA con PKCS#1 v1.5 padding
        var rsa = privateKey; // Assicurati che sia RSACryptoServiceProvider
        if (rsa == null)
        {
            throw new Exception("Chiave non valida.");
        }

        // Dati da criptare
        String stamp = "123456";
        byte[] buffer = Encoding.UTF8.GetBytes(stamp);

        // Cripta i dati con padding PKCS#1
        byte[] encryptedBytes = rsa.Encrypt(buffer, false); // false per PKCS#1 padding

        // Converti in base64
        String encrypted = Convert.ToBase64String(encryptedBytes);
        
        Console.WriteLine("Encrypted: " + encrypted);
    }
}
