<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xslLogFormat="urn:LogFormat">
  <xsl:import href="../helper.xslt" />
  <xsl:output method="html" indent="no" />

  <xsl:template match="age_otp_customerstatusupdafterverifiedotp">
    <table class="tblDesc">
      <xsl:call-template name="renderRow">
        <xsl:with-param name="firstKey" select="'SubAccount'" />
        <xsl:with-param name="firstValue" select="@subaccount" />

        <xsl:with-param name="secondKey" select="'checkedotp'" />
        <xsl:with-param name="secondValue" select="@checkedotp" />

        <xsl:with-param name="thirdKey" select="'FunctionName'" />
        <xsl:with-param name="thirdValue" select="@functionname" />
      </xsl:call-template>

      <xsl:call-template name="renderRow">
        <xsl:with-param name="firstKey" select="'ip'" />
        <xsl:with-param name="firstValue" select="@ip" />

        <xsl:with-param name="secondKey" select="'language'" />
        <xsl:with-param name="secondValue" select="@language" />
      </xsl:call-template>
    </table>
  </xsl:template>
</xsl:stylesheet>
