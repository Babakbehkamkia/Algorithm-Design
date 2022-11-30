# python3
import queue

class Edge:

    def __init__(self, u, v, capacity):
        self.u = u
        self.v = v
        self.capacity = capacity
        self.flow = 0

# This class implements a bit unusual scheme for storing edges of the graph,
# in order to retrieve the backward edge for a given edge quickly.
class FlowGraph:

    def __init__(self, n):
        # List of all - forward and backward - edges
        self.edges = []
        # These adjacency lists store only indices of edges in the edges list
        self.graph = [[] for _ in range(n)]

    def add_edge(self, from_, to, capacity):
        # Note that we first append a forward edge and then a backward edge,
        # so all forward edges are stored at even indices (starting from 0),
        # whereas backward edges are stored at odd indices.
        forward_edge = Edge(from_, to, capacity)
        backward_edge = Edge(to, from_, 0)
        self.graph[from_].append(len(self.edges))
        self.edges.append(forward_edge)
        self.graph[to].append(len(self.edges))
        self.edges.append(backward_edge)

    def size(self):
        return len(self.graph)

    def get_ids(self, from_):
        return self.graph[from_]

    def get_edge(self, id):
        return self.edges[id]

    def add_flow(self, id, flow):
        # To get a backward edge for a true forward edge (i.e id is even), we should get id + 1
        # due to the described above scheme. On the other hand, when we have to get a "backward"
        # edge for a backward edge (i.e. get a forward edge for backward - id is odd), id - 1
        # should be taken.
        #
        # It turns out that id ^ 1 works for both cases. Think this through!
        self.edges[id].flow += flow
        self.edges[id ^ 1].flow -= flow


def read_data():
    vertex_count, edge_count = map(int, input().split())
    # graph = FlowGraph(vertex_count)
    edges=[]
    for _ in range(edge_count):
        l = list(map(int, input().split()))
        # graph.add_edge(u - 1, v - 1, capacity)
        edges.append(l)
    return vertex_count,edge_count,edges


def max_flow(nodeCount,edgeCount,edges):
    # flow = 0
    # # your code goes here
    # q=queue.Queue()
    
    # for i in range(graph.size()):

    # return flow
    maxFlow=0
    adj=makeAdj(nodeCount,edges)
    while(True):
        path=BFS(nodeCount,adj)
        if(path==None):
            break
        currentNode=0
        minimum=999999999999999999
        for i in range(len(path)):
            if(adj[currentNode][path[i]]<minimum):
                minimum=adj[currentNode][path[i]]
            currentNode=path[i]
        currentNode=0
        for i in range(len(path)):
            adj[currentNode][path[i]]-=minimum
            if(adj[currentNode][path[i]]==0):
                del adj[currentNode][path[i]]
            if(currentNode in adj[path[i]]):
                adj[path[i]][currentNode]+=minimum
            else:
                adj[path[i]][currentNode]=minimum
            currentNode=path[i]
        maxFlow+=minimum
    return maxFlow

def makeAdj(nodeCount,edges):
    adj={}
    for i in range(nodeCount):
        adj[i]={}
    for i in range(len(edges)):
        if(edges[i][0]-1==edges[i][1]-1):
            continue
        if((edges[i][1]-1) in adj[edges[i][0]-1]):
            adj[edges[i][0]-1][edges[i][1]-1]+=edges[i][2]
        else:
            adj[edges[i][0]-1][edges[i][1]-1]=edges[i][2]
    return adj



def BFS(nodeCount,adj):
    visited=[]
    parents=[]
    for i in range(nodeCount):
        visited.append(0)
        parents.append(0)
    isExist=False
    q=queue.Queue()
    q.put(0)
    visited[0]=1
    while(not q.empty() and not isExist):
        currentNode=q.get()
        for item in adj[currentNode]:
            if(item==nodeCount-1):
                isExist=True
                parents[item]=currentNode
                break
            if(visited[item]==0):
                visited[item]=1
                q.put(item)
                parents[item]=currentNode
    
    path=[]
    if(isExist==False):
        return None
    parent=nodeCount-1
    while(parent!=0):
        path.append(parent)
        parent=parents[parent]
    path.reverse()
    return path



if __name__ == '__main__':
    vertex_count,edge_count,edges = read_data()
    print(max_flow(vertex_count,edge_count,edges))
