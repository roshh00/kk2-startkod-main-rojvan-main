Den börjar ge mig felmeddelande om att index är utanför arrayen på två ställen. I ShoppingList och i Program. 

Crashen börjar i ShoppingList på linje 90, där jag behövde lägga till en if-sats på raden. 
Nu kan jag köra koden. 

Den ger mig prisen som är skriven på items.txt och en total summa på 121kr
Började med att lägga till en vara och gav den namnet mjölk med priset 15kr. 
Nu är listan sparad med en beskriving av mjölk på 15kr. 
Har lagt till tre varor i menyn med namn och pris men extra siffrorna är fortfarande med när jag kör koden. 

Jag tog bort sifforna i txt filen och behöll dem nya, men nu visar det inte namnet på dem utan bara det nya priset. 
Ändrade i txt filen och gav namn till mjölk, bröd och ost. 

Men nu visar den bara Ost och hur mycket den kostar. 
Frågade Chatbotten i Vs Code och den gav mig att jag skulle ersätta två rader i ShoppingList
Ändrade till String Lines = File.ReadAllLines(path); istället för string text (ReadAllText);

Koden visar beskrivingen på alla tre varor med text och pris. 
nu visar total summan fel pris. 

Jag ser att i ShoppingList på rad 28 så är den står for int (1) då den börjar räkna från index 1. 
Vilket gjorde så den hoppade över första raden. 
Ändrar koden till for int (0) istället för den kan börja räkna från första raden. 

Nu när koden körs så visar den rätt total summa. 

Provade med att lägga till en ny vara
Den la till varan i txt filen och kan köras som vanligt.


La till catch på ShoppingList på rad 74 med FileNotFoundExecption. 
Programmet crashade när jag skrev in ett ogiltigt tecken. Ändrade i program.cs filen
Jag la till tryParse vid inmatning av ogiltigt tecken och nu får man upp felmeddelanden. 


Provade att starta programmet utan items.txt men den kraschade. 
La till på ShoppingList på rad 86 att programmet kan köras utan items.txt fil
Men den skapar en ny items.txt fil när jag väljer att spara mina varor på menyn. 


Jag la till på ShoppingList på rad 73 att listan blir bara sparad om man skriver in rätt
Om ett fel inträffar så visar den ett felmeddelande på att listan kunde inte sparas. 




2. Nu när alla sex fel är lösta ska jag påbörja andra delen. 

Började med att kasta in argument execption med ett felmeddelande i items.cs på rad 14
När man skrev in en tom rad / bara mellanslag. 

Har lagt till ArgumentOutOfRangeExecption i både items.cs och program.cs
När man skriver in ett negativt värde så får vi upp ett felmeddelande

När jag har lagt till ett budgettak så valde jag att lägga till i ShoppingList.cs och i Program.cs
Jag började med i ShoppingList att lägga till en readonly int (budget) i public stringen
Därefter i public void kan dem integrera med varandra om priset överstiger budgettaket. 

I Program.cs la jag til en ytterligare catch för att förhindra programmet till att krascha. 



Klassdiagram länk: https://mermaid.ai/app/projects/ad7c1891-c192-4702-9056-566f9173add5/diagrams/fe57b138-43b6-49d7-8103-fc1d804f31a1/version/v0.1/edit