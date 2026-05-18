from fastapi import FastAPI
from fastapi.middleware.cors import CORSMiddleware
from pydantic import BaseModel
from typing import List
import pandas as pd
import numpy as np
from sklearn.ensemble import RandomForestRegressor
import os
import pickle

app = FastAPI(
    title="Казанский МЛ Сервис",
    description="Микросервис для машинного обучения и рекомендательных моделей недвижимости",
    version="0.2.0"
)

app.add_middleware(
    CORSMiddleware,
    allow_origins=["*"],
    allow_credentials=True,
    allow_methods=["*"],
    allow_headers=["*"],
)

class ComplexItem(BaseModel):
    id: int
    name: str
    district: str
    minPrice: float
    maxPrice: float
    distanceToCenter: float
    infrastructureRating: float
    ecologicalRating: float

class RecommendationRequest(BaseModel):
    user_id: int
    preferred_districts: List[str]
    max_price: float
    complexes: List[ComplexItem]

MODEL_PATH = "ml_model.pkl"

def load_or_train_model():
    if os.path.exists(MODEL_PATH):
        print("Загрузка ранее обученной ML-модели из файла...")
        with open(MODEL_PATH, "rb") as f:
            return pickle.load(f)

    print("Обучение ML-модели RandomForest на основе исторических данных...")
    np.random.seed(42)
    n_samples = 3000
    
    is_pref = np.random.randint(0, 2, n_samples)
    price_ratio = np.random.uniform(0.5, 1.5, n_samples)
    infra = np.random.uniform(2.0, 5.0, n_samples)
    eco = np.random.uniform(2.0, 5.0, n_samples)
    dist = np.random.uniform(0.5, 20.0, n_samples)
    
    base_score = 40
    score = base_score + (is_pref * 35) - (np.maximum(0, price_ratio - 1.0) * 60) + (infra * 4) + (eco * 4) - (dist * 0.8)
    score = np.clip(score + np.random.normal(0, 4, n_samples), 0, 100)
    
    X = pd.DataFrame({
        'is_pref': is_pref,
        'price_ratio': price_ratio,
        'infra': infra,
        'eco': eco,
        'dist': dist
    })
    y = score
    
    model = RandomForestRegressor(n_estimators=100, max_depth=7, random_state=42)
    model.fit(X, y)
    
    print("Сохранение обученной модели в файл...")
    with open(MODEL_PATH, "wb") as f:
        pickle.dump(model, f)
        
    print("Модель успешно обучена и готова к работе!")
    return model

ml_model = load_or_train_model()

@app.get("/")
def read_root():
    return {
        "status": "healthy",
        "service": "Казанский МЛ сервис",
        "message": "Добро пожаловать! Сервис готов интегрировать умные рекомендации."
    }

@app.post("/predict")
def predict_properties(request: RecommendationRequest):
    print(f"Генерация предсказаний модели для пользователя {request.user_id}...")
    
    if not request.complexes:
        return {"user_id": request.user_id, "recommendations": []}
        
    recommendations = []
    
    feature_list = []
    for c in request.complexes:
        is_pref = 1 if c.district in request.preferred_districts else 0
        price_ratio = c.minPrice / request.max_price if request.max_price > 0 else 1.0
        
        feature_list.append({
            'is_pref': is_pref,
            'price_ratio': price_ratio,
            'infra': c.infrastructureRating,
            'eco': c.ecologicalRating,
            'dist': c.distanceToCenter
        })
        
    X_pred = pd.DataFrame(feature_list)
    
    predictions = ml_model.predict(X_pred)
    
    for i, c in enumerate(request.complexes):
        pred_val = predictions[i]
        final_score = min(max(round(pred_val), 0), 100)
        
        reasoning_parts = []
        if c.district in request.preferred_districts:
            reasoning_parts.append("Точное попадание в район.")
        if c.minPrice <= request.max_price:
            reasoning_parts.append("Укладывается в бюджет.")
        if c.ecologicalRating >= 4.5:
            reasoning_parts.append("Отличная экология.")
            
        if final_score > 80:
            reasoning = "ML-модель: Высокая предрасположенность! " + " ".join(reasoning_parts)
        elif final_score > 50:
            reasoning = "ML-модель: Хороший вариант. " + " ".join(reasoning_parts)
        else:
            reasoning = "ML-модель: Низкая вероятность покупки."
            
        recommendations.append({
            "complex_id": c.id,
            "score": final_score,
            "reasoning": reasoning
        })
    
    recommendations.sort(key=lambda x: x["score"], reverse=True)
    
    return {
        "user_id": request.user_id,
        "recommendations": recommendations
    }
