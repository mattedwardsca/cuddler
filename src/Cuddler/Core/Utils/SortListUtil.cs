using Cuddler.Data.Entities;

namespace Cuddler.Core.Utils;

public static class SortListUtil
{
    public static List<ISortable> MoveDown(List<ISortable> list, ISortable item)
    {
        var currentPosition = list.IndexOf(item);
        if (item.SortOrder == list.Count)
        {
            return list; // can't move down!
        }

        var newPosition = currentPosition + 1;
        var sortList = SortList(list, item, newPosition);

        return sortList;
    }

    public static List<ISortable> MoveUp(List<ISortable> list, ISortable item)
    {
        var currentPosition = list.IndexOf(item);
        if (currentPosition < 1)
        {
            return list; // can't move up!
        }

        var newPosition = currentPosition - 1;
        var sortList = SortList(list, item, newPosition);

        return sortList;
    }

    public static List<ISortable> OrderList(List<ISortable> list)
    {
        var newList = (from f in list
                       orderby f.SortOrder
                       select f).ToList();

        var index = 1;
        foreach (var listItem in list)
        {
            listItem.SortOrder = index;
            index++;
        }

        return newList;
    }

    public static List<ISortable> Sort(List<ISortable> list, ISortable? item, int newIndex)
    {
        var sortList = SortList(list, item, newIndex);

        return sortList;
    }

    public static List<ISortable> SortList(List<ISortable> sortList, ISortable? sortable, int newPosition)
    {
        if (sortable == null)
        {
            return sortList;
        }

        if (sortList.Count > 1)
        {
            var exists = sortList.Remove(sortable);
            if (!exists)
            {
                throw new FileNotFoundException();
            }

            sortList.Insert(newPosition, sortable);
            var index = 1;
            foreach (var listItem in sortList)
            {
                listItem.SortOrder = index;
                index++;
            }
        }

        return sortList;
    }
}