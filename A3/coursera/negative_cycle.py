#Uses python3

import sys


def negative_cycle(adj,cost):
    dist=[None for i in range(len(adj))]
    for i in range(len(adj)):
        is_changed=False
        for i in range(len(adj)):
            if dist[i] is None:
                dist[i] = 0
                is_changed=True

        if not is_changed:
            break
    # dist[0]=0
    n=len(adj)
    for i in range(n - 1):
        is_changed=False
        for j in range(n):
            if dist[j] is not None:
                for k in range(len(adj[j])):
                    if dist[adj[j][k]] is None:
                        dist[adj[j][k]]=dist[j]+cost[j][k]
                        is_changed=True
                    else:
                        dist[adj[j][k]]=min(dist[j]+cost[j][k],dist[adj[j][k]])
                        is_changed=True
        if not is_changed:
            break
    for i in range(len(adj)):
        if dist[i] is not None:
            for k in range(len(adj[i])):
                if dist[i]+cost[i][k]<dist[adj[i][k]]:
                    return 1
    return 0


if __name__ == '__main__':
    input = sys.stdin.read()
    data = list(map(int, input.split()))
    n, m = data[0:2]
    data = data[2:]
    edges = list(zip(zip(data[0:(3 * m):3], data[1:(3 * m):3]), data[2:(3 * m):3]))
    data = data[3 * m:]
    adj = [[] for _ in range(n)]
    cost = [[] for _ in range(n)]
    for ((a, b), w) in edges:
        adj[a - 1].append(b - 1)
        cost[a - 1].append(w)
    print(negative_cycle(adj,cost))
