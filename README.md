# .Net Core Api Streaming
Based on [this article](https://medium.com/@sergioprates/criando-uma-api-streaming-com-net-core-b2eeaab0dfac) I created this project to stream some Json.    
It uses [this platform](https://www.4devs.com.br/gerador_de_veiculos) to generate vehicles (in brazilian pattern), make a few conversions and display the result.    
    
## Custom
Create your own service and inherit it from IItemGenerator.cs.    
Remember to add it to ServiceCollection.
