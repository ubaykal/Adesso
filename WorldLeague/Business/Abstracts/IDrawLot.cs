using WorldLeague.ViewModels;

namespace WorldLeague.Business.Abstracts;

public interface IDrawLot
{
    Task<List<DrawLotResponseViewModel>> DrawLotsStart(DrawLotRequestViewModel request);
}