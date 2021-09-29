#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>
#include <stdlib.h>

#define _burst_sort 1
#define _id_sort 1


struct Process
{
	int id;
	int arrival_time;
	int burst_time;

	int work_time;
	int complition_time;
	int finished;
	int started;
};

int comp1(const void * elem1, const void * elem2)
{
	struct Process *proc1 = (struct Process *)elem1;
	struct Process *proc2 = (struct Process *)elem2;
	if (proc1->arrival_time > proc2->arrival_time) return  1;
	if (proc1->arrival_time < proc2->arrival_time) return  -1;
#if _burst_sort==1
	if (proc1->burst_time > proc2->burst_time) return  1;
	if (proc1->burst_time < proc2->burst_time) return  -1;
#endif
#if _id_sort==1
	if (proc1->id > proc2->id) return  1;
	if (proc1->id < proc2->id) return  -1;
#endif

	return 0;
}
int comp_id(const void * elem1, const void * elem2)
{
	struct Process *proc1 = (struct Process *)elem1;
	struct Process *proc2 = (struct Process *)elem2;
	if (proc1->id > proc2->id) return  1;
	if (proc1->id < proc2->id) return  -1;
	return 0;
}

int main()
{
	int life_total_time;
	int n;
	int lifetime[1000][100];
	for (int i = 0; i < 1000; i++)for (int e = 0; e < 100; e++) lifetime[i][e] = 0;
	printf("How much processes you need: ");
	scanf("%d", &n);

	struct Process procs[100];
	for (int i = 0; i < n; i++)
	{
		procs[i].id = i;
		scanf("%d%d", &procs[i].arrival_time, &procs[i].burst_time);
	}
	for (int i = 0; i < n; i++)
	{
		procs[i].work_time = 0;
		procs[i].finished = 0;
		procs[i].started = 0;
	}

	qsort(procs, n, sizeof(struct Process), comp1);

	int f = 0;

	for (int o = 0;; o++)
	{
		life_total_time = o;
		f = 0;
		//decide which process to run
		int to_run = -1;
		for (int i = 0; i < n; i++)
		{
			if (procs[i].finished == 0)
			{
				f = 1;
				if (procs[i].arrival_time <= o)
				{
					to_run = i;
					break;
				}
			}
		}
		//graphics
		for (int i = 0; i < n; i++)
		{
			if (procs[i].arrival_time<=o && procs[i].finished == 0) lifetime[o][procs[i].id] = 1;
		}
		if (to_run != -1) lifetime[o][procs[to_run].id] = 2;

		//run process
		if (f == 0) break;
		if (to_run == -1) continue;
		
		procs[to_run].started = 1;
		procs[to_run].work_time++;
		if (procs[to_run].burst_time == procs[to_run].work_time)
		{
			procs[to_run].finished = 1;
			procs[to_run].complition_time = o + 1;
		}
	}
	qsort(procs, n, sizeof(struct Process), comp_id);
	//output
	printf("\n\nProcess\t CT\t TAT\t WT\n");
	int total_TAT = 0;
	int total_WT = 0;
	for (int i = 0; i < n; i++)
	{
		int id = procs[i].id;
		int completion_time = procs[i].complition_time;
		int turn_around_time = completion_time - procs[i].arrival_time;
		int waiting_time = turn_around_time - procs[i].burst_time;
		printf("%d\t %d\t %d\t %d\t\n", id, completion_time, turn_around_time, waiting_time);
		total_TAT += turn_around_time;
		total_WT += waiting_time;

	}
	printf("Average Turnaround time: %.1f\n", (float)total_TAT / (float)n);
	printf("Average waiting time: %.1f\n", (float)total_WT / (float)n);

	printf("\n");
	for (int i = 0; i < n; i++)
	{
		printf("%2d ", i+1);
		for (int e = 0; e < life_total_time; e++)
		{
			if (lifetime[e][i] == 0)printf(" ");
			if (lifetime[e][i] == 1)printf(".");
			if (lifetime[e][i] == 2)printf("X");
		}
		printf("\n");
	}
}