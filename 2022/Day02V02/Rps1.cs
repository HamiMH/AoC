internal class Rps1 : RpsBase
{
    public Rps1()
    {
        _dict = new Dictionary<string, int>()
        {
            {"A X",Rock+Draw},
            {"B X",Rock+Loss},
            {"C X",Rock+Win},

            {"A Y",Paper+Win},
            {"B Y",Paper+Draw},
            {"C Y",Paper+Loss},

            {"A Z",Sciss+Loss},
            {"B Z",Sciss+Win},
            {"C Z",Sciss+Draw},
        };
    }

}