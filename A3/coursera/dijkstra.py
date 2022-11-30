#Uses python3

import sys
import queue
import heapq


def distance(adj, cost, s, t):
    #write your code here
    
    c=[float('inf') for i in range(len(adj))]
    c[s]=0
    # q=queue.PriorityQueue()
    nodes=[]
    
    for i in range (len(adj)-1,-1,-1):
        if i==s:
            continue
        nodes.append(i)
    nodes.append(s)
    # q.put([s,c[s]])
    # heapq.heapify(q)
    # visited=[]
    # for i in range(len(adj)):
    #     list_to_add=[]
    #     for j in range(len(adj)):
    #         list_to_add.append(False)
    #     visited.append(list_to_add)
    # heapq.heappush(q,[0,s])
    # for i in range(len(adj)):
    #     if i==s:
    #         continue
    #     heapq.heappush(q,[999999,i])
    # q.join()
    # visited[j]=True
    # cost[j]=0
    # reached=False
    while len(nodes):
        # node=q.get()
        node=nodes.pop()
        # node=heapq.heappop(q)
        for i in range(len(adj[node])):
            node2_index=adj[node][i]
            # if visited[i]==True and order[node]%2==order[i]%2 :
            #     return 0
            # visited[i]=True
            # if visited[node][node2]==False:
            #     visited[node][node2]=True
                # visited[node2][node]=True
            if c[node2_index]>c[node]+cost[node][i]:
                c[node2_index]=c[node]+cost[node][i]
                # q.put([node2_index,c[node2_index]])
                # if i==t:
                #     reached=True
                #     break
                # new_q=[]
                # for j in range(len(q)):
                #     a=heapq.heappop(q)
                #     new_q.append([c[a[1]],a[1]])
                    # heapq.heapreplace(q,[c[j],j])
                # q=new_q
                index=0
                for j in range(len(nodes)):
                    if nodes[j]==node2_index:
                        index=j
                        break
                for k in range(index,len(nodes)-1):
                    if c[nodes[k]]>=c[nodes[k+1]]:
                        # for a in range(index,k):
                        #     nodes[a],nodes[a+1]=nodes[a+1],nodes[a]
                        break
                    else:
                        nodes[k],nodes[k+1]=nodes[k+1],nodes[k]
                    
        # if reached:
        #     break
    if c[t]==float('inf'):
        return -1
    return c[t]


if __name__ == '__main__':
    input = sys.stdin.read() 
    # no_of_lines = 11
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
    s, t = data[0] - 1, data[1] - 1
    print(distance(adj, cost, s, t))
