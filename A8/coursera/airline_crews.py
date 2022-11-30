# python3
import queue
class MaxMatching:
    def read_data(self):
        n, m = map(int, input().split())
        adj_matrix = [list(map(int, input().split())) for i in range(n)]
        return n,m,adj_matrix

    def write_response(self, matching):
        line = [str(-1 if x == -1 else x) for x in matching]
        print(' '.join(line))

    def find_matching(self,flightCount,crewCount, info):
        # Replace this code with an algorithm that finds the maximum
        # matching correctly in all cases.
        # n = len(adj_matrix)
        # m = len(adj_matrix[0])
        # matching = [-1] * n
        # busy_right = [False] * m
        # for i in range(n):
        #     for j in range(m):
        #         if adj_matrix[i][j] and matching[i] == -1 and (not busy_right[j]):
        #             matching[i] = j
        #             busy_right[j] = True
        # return matching
        match =[]
        for i in range(flightCount):
            match.append(0)
        nodeCount=flightCount+crewCount+2
        adj=self.makeAdj(flightCount,crewCount,info)
        while(True):
            path=self.BFS(flightCount,nodeCount,adj,match)
            if(path==None):
                break
            currentNode=0
            minimum=99999999999999999999
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
        for i in range(flightCount):
            if(match[i]==0):
                match[i]=-1
        return match



    def makeAdj(self,flightCount,crewCount,edges):
        nodeCount=flightCount+crewCount
        adj={}
        for i in range(nodeCount+2):
            adj[i]={}
        for i in range(1,flightCount+1):
            adj[0][i]=1
        for i in range(flightCount+1,nodeCount+1):
            adj[i][nodeCount+1]=1
        for i in range(flightCount):
            for j in range(crewCount):
                if(edges[i][j]==1):
                    adj[i+1][flightCount+j+1]=1
        return adj




    # def BFS(self,flightCount,,adj):
    #     visited=[]
    #     parents=[]
    #     for i in range(nodeCount):
    #         visited.append(0)
    #         parents.append(0)
    #     isExist=False
    #     q=queue.Queue()
    #     q.put(0)
    #     visited[0]=1
    #     while(len(q) and not isExist):
    #         currentNode=q.get()
    #         for item in adj[currentNode].Keys:
    #             if(item==nodeCount-1):
    #                 isExist=True
    #                 parents[item]=currentNode
    #                 break
    #             if(visited[item]==0):
    #                 visited[item]=1
    #                 q.put(item)
    #                 parents[item]=currentNode
        
    #     path=[]
    #     if(isExist==False):
    #         return None
    #     parent=nodeCount-1
    #     while(parent!=0):
    #         path.append(parent)
    #         parent=parents[parent]
    #     path.reverse()
    #     return path

    def BFS(self,flightCount,nodeCount,adj,match):
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
        for i in range(int((len(path)-1)/2)):
            match[path[2*i]-1]=path[2*i+1]-flightCount
        return path

    def solve(self):
        n,m,adj_matrix = self.read_data()
        matching = self.find_matching(n,m,adj_matrix)
        self.write_response(matching)

if __name__ == '__main__':
    max_matching = MaxMatching()
    max_matching.solve()
