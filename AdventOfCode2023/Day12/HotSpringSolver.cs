using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day12;

internal class HotSpringSolver
{
    private readonly SpringRow _springRow;
    private readonly SpringRowArrangementComparer _comparer = new();

    public HotSpringSolver(SpringRow springRow)
    {
        _springRow = springRow;
    }

    public IReadOnlyList<SpringRow> Arrangements => _arrangements.Distinct(_comparer).ToList();
    private List<SpringRow> _arrangements = new();

    public void Solve()
    {
        DoIt(_springRow.Statuses, _springRow.DamagedGroups, new List<SpringStatus>());
    }

    private void DoIt(
        List<SpringStatus> statuses,
        List<int> damagedGroups,
        List<SpringStatus> currentPath)
    {
        if (!damagedGroups.Any())
        {
            if (statuses.Any(s => s == SpringStatus.Damaged))
            {
                // Bad - this meant there were some left over. They should fit *exactly*
                // into the groups.
                return;
            }

            var foundArrangement = new SpringRow()
            {
                Statuses = currentPath,
                DamagedGroups = _springRow.DamagedGroups
            };
            _arrangements.Add(foundArrangement);
            return;
        }

        if (!statuses.Any())
        {
            return;
        }

        if (statuses[0] == SpringStatus.Operational)
        {
            DoIt(statuses.Skip(1).ToList(), damagedGroups, Track(currentPath, SpringStatus.Operational));
            return;
        }

        if (statuses[0] == SpringStatus.Unknown)
        {
            DoIt(
                ReplaceFirst(statuses, SpringStatus.Operational),
                damagedGroups,
                currentPath
            );

            DoIt(
               ReplaceFirst(statuses, SpringStatus.Damaged),
               damagedGroups,
               currentPath
           );
            return;
        }


        if (StartsWithSizedDamagedGroup(statuses, damagedGroups[0]))
        {
            var current = Track(currentPath, SpringStatus.Damaged, damagedGroups[0]);

            if (statuses.Count != damagedGroups[0])
            {
                current.Add(SpringStatus.Operational);
            }

            DoIt(
                statuses.Skip(damagedGroups[0] + 1).ToList(),
                damagedGroups.Skip(1).ToList(),
                current
            );
            return;
        }

        // If we have a damanged set that doesn't fit into the next group, then this
        // path is invalid.
        return;
    }

    private bool StartsWithSizedDamagedGroup(List<SpringStatus> statuses, int size)
    {
        if (size > statuses.Count)
        {
            return false;
        }

        // Group ends with the end of the row
        if (statuses.Count == size && statuses.All(Damaged))
        {
            return true;
        }

        // Must be size number of damaged, followed by undamaged
        List<SpringStatus> group = statuses
            .Take(size)
            .ToList();

        return group.All(Damaged) && Operational(statuses[size]);
    }

    [DebuggerStepThrough]
    private bool Damaged(SpringStatus status)
    {
        return status == SpringStatus.Damaged || status == SpringStatus.Unknown;
    }

    [DebuggerStepThrough]
    private bool Operational(SpringStatus status)
    {
        return status == SpringStatus.Operational || status == SpringStatus.Unknown;
    }

    [DebuggerStepThrough]
    private List<SpringStatus> ReplaceFirst(
        List<SpringStatus> original,
        SpringStatus newStatus)
    {
        var newSequence = new List<SpringStatus>() { newStatus};
        newSequence.AddRange(original.Skip(1));
        return newSequence;
    }

    [DebuggerStepThrough]
    private List<SpringStatus> Track(
        List<SpringStatus> current,
        SpringStatus newStatus,
        int count = 1
        )
    {
        var newSequence = new List<SpringStatus>(current);
        newSequence.AddRange(Enumerable.Repeat(0, count).Select([DebuggerStepThrough] (_) => newStatus));

        return newSequence;
    }
}

internal class SpringRowArrangementComparer : IEqualityComparer<SpringRow>
{
    public bool Equals(SpringRow? x, SpringRow? y)
    {
        return x.Statuses.SequenceEqual(y.Statuses);
    }

    public int GetHashCode([DisallowNull] SpringRow obj)
    {
        return 1; // always use Equals
    }
}