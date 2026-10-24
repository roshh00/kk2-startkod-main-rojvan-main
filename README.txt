Genomgång av felen och lösningarna

Här beskriver jag några problem jag stötte på, hur jag löste dem och varför
jag valde de lösningarna.

Klassdiagram:

https://mermaid.ai/app/projects/ad7c1891-c192-4702-9056-566f9173add5/diagrams/25257707-967a-45c8-a0f8-270d3b9e5a4f/share/invite/eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJkb2N1bWVudElEIjoiMjUyNTc3MDctOTY3YS00NWM4LWEwZjgtMjcwZDNiOWU1YTRmIiwiYWNjZXNzIjoiRWRpdCIsInB1cnBvc2UiOiJzaGFyZS1pbnZpdGUiLCJpYXQiOjE3OTI4NDE3NzUsImV4cCI6MTc5NTQzMzc3NX0.aIMUDpCXWnQfN1uG4eB2PXxVE_iB_GqLLo2Qa3sv7Wg?entryPoint=share-modal

## Raderna i items.txt

Varje rad i items.txt ska innehålla ett pris och ett namn med semikolon
emellan. När jag läste in filen fick jag problem om en rad saknade namn
eller inte hade rätt format. Då kunde programmet försöka läsa en del som
inte fanns, till exempel parts[1]. Nu kontrollerar Load raden först och
hoppar över rader som inte går att använda.

## Läsa filen rad för rad

Filen har en vara per rad, så jag använder File.ReadAllLines för att läsa
in den som en samling rader. Då kan programmet dela upp och kontrollera
varje vara för sig. När jag tidigare behandlade filen som en enda text blev
det svårt att få ut rätt pris och namn.

## Totalsumman och numreringen

Listans index börjar på 0. När summeringen började på fel index kom inte
alla varor med i summan. Jag ändrade så att den börjar från index 0.
Numren som visas i menyn börjar däremot på 1, eftersom det blir enklare
för användaren. Vid borttagning måste menyvalet därför göras om till ett
index genom att dra bort 1.

## När inmatningen inte är ett heltal

ReadInt använder int.TryParse för att kontrollera inmatningen. Om jag
skriver text i stället för ett heltal får jag ett felmeddelande och kan
försöka igen. 
TryParse säger också nej till tal som är för stora för typen
int.

## Om filen saknas eller innehåller fel

Om items.txt saknas avslutar Load utan att försöka läsa den. Om filen finns
men någon rad är felaktig hoppas den raden över. På så sätt kan programmet
starta även om filen inte finns och en felaktig rad stoppar inte
inläsningen av resten.

## Om det inte går att spara

Save fångar fel som kan uppstå när filen skrivs, till exempel om programmet
inte har rättighet att skriva till platsen. Då visas ett felmeddelande i
stället för att programmet kraschar utan förklaring.

## Namn och pris

Ett namn får inte vara tomt eller bara innehålla mellanslag.
Om namnet inte godkänns visar programmet felet och frågar efter ett nytt namn. 
Priset får inte heller vara negativt. Item stoppar det med ArgumentOutOfRangeException,
som fångas i Program så att användaren kan skriva in ett nytt pris.

## Budgeten och valet av undantag

ShoppingList.Add kontrollerar om den nya varan skulle göra att totalsumman
blir högre än budgeten. Om det händer läggs varan inte till, utan metoden
kastar InvalidOperationException. Program fångar undantaget och visar
meddelandet för användaren.

Jag valde ett undantag eftersom Add då kan förklara varför den inte kunde
lägga till varan.
Den som anropar metoden hade sedan behövt ordna felmeddelandet separat. 
Ett undantag är alltså ett val för hur felet ska skickas vidare.
I programmet fångas det direkt där meddelandet visas.

## Val utanför menyn

ReadInt kontrollerar att inmatningen är ett heltal, men inte att det är ett
menyalternativ. Därför har menyn också en kontroll som visar ett
felmeddelande om talet inte är mellan 1 och 5. Då får användaren veta att
valet inte finns.

## Nummer som inte finns vid borttagning

RemoveAt kontrollerar att numret är mellan 1 och antalet varor. Om numret
inte stämmer kastas ArgumentOutOfRangeException, som fångas i Program.
Användaren får då veta att det inte finns någon vara med det numret.

Jag hade också råkat anropa RemoveAt två gånger för samma nummer. Det
andra anropet låg utanför try/catch och kunde därför krascha programmet
även om det första anropets fel redan hade fångats. Jag tog bort det extra
anropet så att borttagningen bara görs en gång.