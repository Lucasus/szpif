using  Microsoft.SqlServer.Server;
using System.Text.RegularExpressions;
 

// klasa zajmujaca sie sprawdzaniem czy jakis tekst pasuje do wyrazenia regularnego 
// wywolywana jako procedura CLR w bazie danych
public class RegularExpressions{

    [Microsoft.SqlServer.Server.SqlFunction()]
    public static bool RegExMatch(string Input, string Pattern) {
        Regex RegexInstance = new Regex(Pattern);
        return RegexInstance.IsMatch(Input);
    }
}

 
