#Uses python3

import sys


def number_of_components(adj):
    result = 0
    #write your code here
    visited=[False for i in range(len(adj))]
    for i in range(len(adj)):
        if visited[i]==False:
            visited[i]=True
            explore(adj,visited,i)
            
            result+=1
    return result
def explore(adj,visited,index):
    for i in range(len(adj[index])):
        if visited[adj[index][i]]==False:
            visited[adj[index][i]]=True
            explore(adj,visited,adj[index][i])
            
if __name__ == '__main__':
    input = sys.stdin.read()
    data = list(map(int, input.split()))
    n, m = data[0:2]
    data = data[2:]
    
    
    # n,m=list(map(int, input().split()))
    # data=[]
    # for i in range(m):
    #     d=list(map(int, input().split()))
    #     data+=d
    edges = list(zip(data[0:(2 * m):2], data[1:(2 * m):2]))
    adj = [[] for _ in range(n)]
    for (a, b) in edges:
        adj[a - 1].append(b - 1)
        adj[b - 1].append(a - 1)
    
    print(number_of_components(adj))
