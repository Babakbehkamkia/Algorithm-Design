#Uses python3

import sys

sys.setrecursionlimit(200000)


def number_of_strongly_connected_components(adj,adj_reverse):
    result = 0
    #write your code here
    visited=[False for i in range(len(adj))]
    visited_inverse=[False for i in range(len(adj))]
    post=[]
    for i in range(len(adj_reverse)):
        if visited_inverse[i]==False:
            visited_inverse[i]=True
            explore_inverse(adj_reverse,visited_inverse,i,post)
    post.reverse()
    index=0
    while index<len(post):
        if visited[post[index]]==False:
            visited[post[index]]=True
            explore(adj,visited,post[index])
            result+=1
        index+=1
    return result

# def acyclic(adj,adj_reverse):
#     visited=[False for i in range(len(adj))]
#     visited_inverse=[False for i in range(len(adj))]
#     post=[]
#     for i in range(len(adj_reverse)):
#         if visited_inverse[i]==False:
#             visited_inverse[i]=True
#             explore_inverse(adj_reverse,visited_inverse,i,post)
#     post.reverse()
#     index=0
#     while index<len(post):
#         if visited[post[index]]==False:
#             visited[post[index]]=True
#             explore(adj,visited,post[index])
#         result+=1
#         index+=1
#     return result

def explore_inverse(adj,visited,index,post):
    for i in range(len(adj[index])):
        if visited[adj[index][i]]==False:
            visited[adj[index][i]]=True
            explore_inverse(adj,visited,adj[index][i],post)
    post.append(index)

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
    edges = list(zip(data[0:(2 * m):2], data[1:(2 * m):2]))
    adj = [[] for _ in range(n)]
    adj_reverse = [[] for _ in range(n)]
    for (a, b) in edges:
        adj[a - 1].append(b - 1)
        adj_reverse[b-1].append(a-1)
    print(number_of_strongly_connected_components(adj,adj_reverse))
