from fastapi import FastAPI
from fastapi.middleware.cors import CORSMiddleware
from pydantic import BaseModel
from typing import List, Dict, Any

app = FastAPI(
    title="Казанский МЛ Сервис",
    description="икросервис для машинного обучения и рекомендательных моделей недвижимости",
    version="0.1.0"
)

# Allow requests from frontend/backend
app.add_middleware(
    CORSMiddleware,
    allow_origins=["*"],
    allow_credentials=True,
    allow_methods=["*"],
    allow_headers=["*"],
)

class RecommendationRequest(BaseModel):
    user_id: int
    preferred_districts: List[str]
    max_price: float
    min_rooms: int

@app.get("/")
def read_root():
    return {
        "status": "healthy",
        "service": "Казанский МЛ сервис",
        "message": "Добро пожаловать! Сервис готов интегрировать умные рекомендации."
    }

@app.post("/predict")
def predict_properties(request: RecommendationRequest):
    # This is a placeholder mock for ML recommendation model
    print(f"Генерация рекомендаций для пользователя {request.user_id}...")
    
    mock_recommendations = [
        {
            "complex_id": 101,
            "score": 0.95,
            "reasoning": "Подходит под предпочитаемые районы и хорошо укладывается в" + str(request.max_price)
        },
        {
            "complex_id": 105,
            "score": 0.88,
            "reasoning": "Подходит под предпочитаемые районы и хорошо укладывается в бюджет"
        }
    ]
    
    return {
        "user_id": request.user_id,
        "recommendations": mock_recommendations
    }
