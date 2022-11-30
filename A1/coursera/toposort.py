#Uses python3

import sys

def explor(adj, visited, post, index):
    for i in range(len(adj[index])):
        if visited[adj[index][i]]==False:
            visited[adj[index][i]]=True
            explor(adj,visited,post,adj[index][i])
    post.append(index)

def toposort(adj):
    # used = [0] * len(adj)
    visited=[False for i in range(len(adj))]
    post = []
    for i in range(len(adj)):
        if visited[i]==False:
            visited[i]=True
            explor(adj,visited,post,i)
    return reversed(post)

if __name__ == '__main__':
    input = sys.stdin.read()
    data = list(map(int, input.split()))
    n, m = data[0:2]
    data = data[2:]
    edges = list(zip(data[0:(2 * m):2], data[1:(2 * m):2]))
    adj = [[] for _ in range(n)]
    for (a, b) in edges:
        adj[a - 1].append(b - 1)
    order = toposort(adj)
    for x in order:
        print(x + 1, end=' ')

