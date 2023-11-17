using Microsoft.CodeAnalysis.CSharp;
using WorldLeague.Business.Abstracts;
using WorldLeague.Entities;
using WorldLeague.Helpers;
using WorldLeague.Repositories;
using WorldLeague.ViewModels;

namespace WorldLeague.Business.Services;

public class DrawLot : IDrawLot
{
    private readonly IUnitofWork _uow;
    private readonly IGenericRepository<Team> _teamRep;
    private readonly IGenericRepository<Drawer> _drawerRep;

    public DrawLot(IUnitofWork uow, IGenericRepository<Team> teamRep, IGenericRepository<Drawer> drawerRep)
    {
        _uow = uow;
        _teamRep = teamRep;
        _drawerRep = drawerRep;
    }

    public async Task<List<DrawLotResponseViewModel>> DrawLotsStart(DrawLotRequestViewModel requestViewModel)
    {
        var groups = AdessoHelper.GetGroup();
        var countries = AdessoHelper.GetCountry();

        if (requestViewModel.GroupCount != 4 && requestViewModel.GroupCount != 8)
            throw new Exception("Grup sayısı 4 veya 8 olmalıdır");

        if (string.IsNullOrEmpty(requestViewModel.DrawerName) || string.IsNullOrEmpty(requestViewModel.DrawerSurname))
            throw new Exception("Kura çekilişi yapan kişinin ad soyad kısmı boş olamaz");

        var teams = CreateTeams(countries);

        var drawResults = DrawLots(groups, teams, requestViewModel.GroupCount);

        var response = ShowResults(drawResults);

        var drawer = GetDrawer(requestViewModel);

        await _teamRep.AddRangeAsync(teams);
        await _drawerRep.AddAsync(drawer);
        
        await _uow.SaveChangesAsync();

        return response;
    }

    private Drawer GetDrawer(DrawLotRequestViewModel requestViewModel)
    {
        var result = new Drawer
        {
            Name = requestViewModel.DrawerName,
            Surname = requestViewModel.DrawerSurname
        };

        return result;
    }

    private static List<DrawViewModel> DrawLots(List<char> groups, List<Team> teams, int groupCount)
    {
        var drawResults = new List<DrawViewModel>();
        var random = new Random();

        for (var i = 1; i <= groupCount; i++)
        {
            foreach (var group in groups)
            {
                var selectedTeam = teams[random.Next(teams.Count)];
                teams.Remove(selectedTeam);
                selectedTeam.Group = group;

                drawResults.Add(new DrawViewModel
                {
                    Group = group,
                    Team = selectedTeam
                });
            }
        }

        return drawResults;
    }


    private static List<Team> CreateTeams(List<string> countries)
    {
        var teams = new List<Team>();

        foreach (var country in countries)
        {
            teams.Add(new Team {Country = country, Name = AdessoHelper.TeamName + GenerateCityName()});
            teams.Add(new Team {Country = country, Name = AdessoHelper.TeamName + GenerateCityName()});
            teams.Add(new Team {Country = country, Name = AdessoHelper.TeamName + GenerateCityName()});
            teams.Add(new Team {Country = country, Name = AdessoHelper.TeamName + GenerateCityName()});
        }

        return teams;
    }

    private static List<DrawLotResponseViewModel> ShowResults(List<DrawViewModel> drawResults)
    {
        var drawLotResponse = new List<DrawLotResponseViewModel>();
        foreach (var result in drawResults)
        {
            var drawResponse = new DrawLotResponseViewModel();

            drawResponse.Group = result.Group;
            drawResponse.Team = new TeamViewModel
            {
                Name = result.Team.Name,
                Country = result.Team.Country
            };

            drawLotResponse.Add(drawResponse);
        }

        return drawLotResponse;
    }

    private static string GenerateCityName()
    {
        string[] cities = {"Istanbul", "Berlin", "Paris", "Amsterdam", "Lisbon", "Rome", "Seville", "Brussels"};
        return cities[new Random().Next(cities.Length)];
    }
}