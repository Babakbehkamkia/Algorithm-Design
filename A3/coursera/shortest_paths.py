#Uses python3

import sys
import queue


def shortet_paths(adj, cost, s, distance, reachable, shortest):
    distance[s]=0
    reachable[s]=1
    n=len(adj)
    #write your code here
    # dist=[None for i in range(len(adj))]
    # dist[s]=0

    for i in range(n - 1):
        is_changed=False
        for j in range(n):
            if reachable[j] == 1:
                for k in range(len(adj[j])):
                    if reachable[adj[j][k]]==0:
                        distance[adj[j][k]]=distance[j]+cost[j][k]
                        reachable[adj[j][k]]=1
                        is_changed=True
                    else:
                        distance[adj[j][k]]=min(distance[j]+cost[j][k],distance[adj[j][k]])
                        is_changed=True
        if not is_changed:
            break
    for i in range(n):
        if reachable[i] == 1:
            for k in range(len(adj[i])):
                if distance[i] + cost[i][k] < distance[adj[i][k]]:
                    distance[adj[i][k]] = distance[i] + cost[i][k]
                    shortest[adj[i][k]] = 0
    for i in range(n):
        if shortest[i] == 0:
            neg_cycle(i, shortest, adj)

def neg_cycle(x, shortest, adj):
    shortest[x] = 0
    for i in adj[x]:
        if shortest[i] == 1:
            neg_cycle(i, shortest, adj)

if __name__ == '__main__':
    input = sys.stdin.read()
    # no_of_lines = 6
    # lines = ""
    # for i in range(no_of_lines):
    #     lines+=input()+"\n"
    # input=lines
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
    s = data[0]
    s -= 1
    distance = [10**19] * n
    reachable = [0] * n
    shortest = [1] * n
    shortet_paths(adj, cost, s, distance, reachable, shortest)
    for x in range(n):
        if reachable[x] == 0:
            print('*')
        elif shortest[x] == 0:
            print('-')
        else:
            print(distance[x])

