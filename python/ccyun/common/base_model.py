from datetime import datetime

from sqlalchemy.ext.declarative import declarative_base
from sqlalchemy import Integer, DateTime, Column, Sequence

Base = declarative_base()


class BaseModel(Base):
    __abstract__ = True
    id = Column(Integer, Sequence("id"), primary_key=True)
    date_added = Column(DateTime, default=datetime.utcnow)
