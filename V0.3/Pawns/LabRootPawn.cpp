// Fill out your copyright notice in the Description page of Project Settings.

#include "LabRootPawn.h"

void ALabRootPawn::BeginPlay()
{
    Super::BeginPlay();
    
    FHumanSetupInfo BoyInfo = {true, 1, 1, 1, 1, 1, true, 0, 1, FLinearColor(1,1,0.5), FLinearColor(1,0,0), FLinearColor(0,0,1), FLinearColor(0,1,0)};
    BoyPawn = GetWorld()->SpawnActor<AHumanPawn>(FVector(200, -50, 0), FRotator::ZeroRotator);
    BoyPawn->Setup(&BoyInfo);
    
    FHumanSetupInfo GirlInfo = {false, 1, 1, 1, 1, 1, true, 1, 1, FLinearColor(1,1,0.5), FLinearColor(0,1,1), FLinearColor(1,1,0), FLinearColor(1,0,1)};
    GirlPawn = GetWorld()->SpawnActor<AHumanPawn>(FVector(200, 50, 0), FRotator::ZeroRotator);
    GirlPawn->Setup(&GirlInfo);
}

void ALabRootPawn::HumanIdle()
{
    BoyPawn->CommandReset();
    GirlPawn->CommandReset();
}

void ALabRootPawn::HumanWalk()
{
    BoyPawn->CommandWalk(200);
    GirlPawn->CommandWalk(200);
}

void ALabRootPawn::HumanRotate()
{
    BoyPawn->CommandRotate(60);
    GirlPawn->CommandRotate(60);
}
