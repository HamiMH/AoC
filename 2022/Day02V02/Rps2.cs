internal class Rps2 : RpsBase
{
    public Rps2()
    {
        _dict = new Dictionary<string, int>()
        {
            {"A X",Sciss+Loss},
            {"B X",Rock+Loss},
            {"C X",Paper+Loss},

            {"A Y",Rock+Draw},
            {"B Y",Paper+Draw},
            {"C Y",Sciss+Draw},

            {"A Z",Paper+Win},
            {"B Z",Sciss+Win},
            {"C Z",Rock+Win},
        };
    }
}