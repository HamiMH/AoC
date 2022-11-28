using System.Data;
using System.Net.Sockets;

internal class TicketValidator
{

    public List<TicRule> Rules { get; set; }
    public Ticket MyTicket { get; set; }
    public List<Ticket> NearTickets { get; set; }
    public List<Ticket> ValidTickets { get; set; }

    public TicketValidator()
    {
        Rules = new List<TicRule>();
        NearTickets = new List<Ticket>();
        ValidTickets = new List<Ticket>();
    }

    internal void AddMyTicket(string v)
    {
        MyTicket = new Ticket(v);
    }

    internal void AddNearbyTicket(string v)
    {
        NearTickets.Add(new Ticket(v));
    }

    internal void AddRule(string str)
    {
        Rules.Add(new TicRule(str));
    }

    internal long GetErrorRate()
    {
        long errorRate = 0;
        foreach (Ticket ticket in NearTickets)
        {
            foreach (int item in ticket.Items)
            {
                if (ItemIsInRangesOfRules(item) == false)
                    errorRate += (long)item;
            }
        }
        return errorRate;
    }
     internal long GetDepartRate()
    {
        long errorRate = 1;
        GetValidTIckets();
        FindPossibleRuleOrders();
        if (FindRulesOrder() == false)
            throw new Exception("Orders doesnt match");

        foreach(TicRule rule in Rules)
        {
            if (rule.Name.Contains("departure"))
                errorRate *= MyTicket.Items[rule.OrderOfRule];
        }
        return errorRate;
    }

    private bool FindRulesOrder()
    {
        bool[]used=new bool[20];
        for(int i = 0; i < 20;i++)
            used[i]=false;

        return TrySetRule(0, used);


    }

    private bool TrySetRule(int indOfRule, bool[] used)
    {
        if (indOfRule == 20)
            return true;
        TicRule rule = Rules[indOfRule];


        for(int i =0; i < 20; i++)
        {
            if (used[i])
                continue;
            if (rule.CanBeRuleOf[i])
            {
                used[i]=true;
                rule.OrderOfRule = i;
                if (TrySetRule(indOfRule + 1, used))
                    return true;
                used[i]=false;
            }

        }
        return false;
    }

    private void FindPossibleRuleOrders()
    {
        foreach (Ticket ticket in ValidTickets)
        {
            for (int i = 0; i < ticket.Items.Count(); i++)
                foreach (TicRule rule in Rules)
                    rule.ItemHasOrderN(ticket.Items[i], i);
        }
    }

    internal void GetValidTIckets()
    {
        bool valid;
        foreach (Ticket ticket in NearTickets)
        {
            valid = true;
            foreach (int item in ticket.Items)
            {
                if (ItemIsInRangesOfRules(item) == false)
                    valid = false;
            }
            if (valid)
                ValidTickets.Add(ticket);
        }

    }

    public bool ItemIsInRangesOfRules(int item)
    {
        foreach (TicRule rule in Rules)
        {
            if (rule.ItemIsInRanges(item))
                return true;
        }
        return false;
    }
}