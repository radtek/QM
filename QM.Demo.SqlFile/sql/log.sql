/***********************************************************************
 ģ��DB ����
***********************************************************************/

--��������
COLUMN TC NEW_VALUE FILENAME;
COLUMN STRTIME NEW_VALUE STIME;
COLUMN ENDTIME NEW_VALUE ETIME;
--���������
VARIABLE IDX   VARCHAR2 (100);
VARIABLE V_SQLERR VARCHAR2(255);
--���������־����·��

SELECT TO_CHAR (SYSDATE, 'MMDD_hh24') || '_rpt.txt' TC FROM DUAL;

SPOOL  C:\QM\FILE\&FILENAME;
--����DBMS OUTPUT
SET SERVEROUTPUT ON;
--����ʼʱ��

SELECT '''' || TO_CHAR (SYSDATE, 'mm-dd hh24:mi:ss') || '''' STRTIME
  FROM DUAL;

--��������

BEGIN
   SELECT IT_SEQ.NEXTVAL INTO :IDX FROM DUAL;

   INSERT INTO QM_TASKLOG
        VALUES ( :IDX,
                :IDX || 'ID',
                'NA',
                SYSDATE,
                :IDX || 'MESSAGE',
				'NA');

   COMMIT;
   --ģ�����������10��
   --ע��ʹ��Ȩ��
   DBMS_LOCK.SLEEP (1);
--�쳣���
EXCEPTION
   WHEN OTHERS
   THEN
      :V_SQLERR := SUBSTR (SQLERRM, 1, 255);
      DBMS_OUTPUT.PUT_LINE ( :V_SQLERR);
END;
/

--�������ʱ��

SELECT '''' || TO_CHAR (SYSDATE, 'mm-dd hh24:mi:ss') || '''' ENDTIME
  FROM DUAL;

SELECT    TRUNC (
               TO_NUMBER (
                    TO_DATE (&ETIME, 'mm-dd hh24:mi:ss')
                  - TO_DATE (&STIME, 'mm-dd hh24:mi:ss'))
             * 24
             * 60,
             2)
       || 'Mins'
          TOTAL
  FROM DUAL;

SET SERVEROUTPUT OFF;
SPOOL OFF;
--�����˳�
EXIT;